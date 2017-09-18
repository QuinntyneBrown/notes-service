﻿using Owin;
using System.Web.Http;
using Microsoft.Owin;
using Unity.WebApi;
using Microsoft.Practices.Unity;
using NotesService.Features.Core;
using Microsoft.ServiceBus.Messaging;
using static Newtonsoft.Json.JsonConvert;
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNet.SignalR;
using NotesService.Features.Notes;
using NotesService.Features.Tags;

[assembly: OwinStartup(typeof(NotesService.Startup))]

namespace NotesService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(config =>
            {
                var container = UnityConfiguration.GetContainer();
                config.DependencyResolver = new UnityDependencyResolver(container);
                ApiConfiguration.Install(config, app);


                var client = SubscriptionClient.CreateFromConnectionString(CoreConfiguration.Config.EventQueueConnectionString, CoreConfiguration.Config.TopicName, CoreConfiguration.Config.SubscriptionName);
                var notesEventBusMessageHandler = container.Resolve<INotesEventBusMessageHandler>();
                var tagsEventBusMessageHandler = container.Resolve<ITagsEventBusMessageHandler>();
                client.OnMessage(message =>
                {
                    try
                    {
                        var messageBody = ((BrokeredMessage)message).GetBody<string>();
                        var messageBodyObject = DeserializeObject<JObject>(messageBody, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                            TypeNameHandling = TypeNameHandling.All,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });

                        notesEventBusMessageHandler.Handle(messageBodyObject);
                        tagsEventBusMessageHandler.Handle(messageBodyObject);

                        GlobalHost.ConnectionManager.GetHubContext<EventHub>().Clients.All.events(messageBodyObject);
                    }
                    catch (Exception e)
                    {

                    }
                });
            });
        }
    }
}