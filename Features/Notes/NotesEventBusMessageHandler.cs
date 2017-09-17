using NotesService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;
using MediatR;
using Newtonsoft.Json;


namespace NotesService.Features.Notes
{
    public interface INotesEventBusMessageHandler: IEventBusMessageHandler { }

    public class NotesEventBusMessageHandler: INotesEventBusMessageHandler
    {
        public NotesEventBusMessageHandler(ICache cache, IMediator mediator)
        {
            _cache = cache;
            _mediator = mediator;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["type"]}" == NotesEventBusMessages.AddedOrUpdatedNoteMessage)
                {
                    _cache.Remove(NotesCacheKeyFactory.Get(new Guid(message["tenantUniqueId"].ToString())));
                }

                if ($"{message["type"]}" == NotesEventBusMessages.RemovedNoteMessage)
                {
                    _cache.Remove(NotesCacheKeyFactory.Get(new Guid(message["tenantUniqueId"].ToString())));
                }

                if ($"{message["type"]}" == NotesEventBusMessages.TryToAddOrUpdateNoteMessage)
                {
                    var addOrUpdateRequest = JsonConvert.DeserializeObject<AddOrUpdateNoteCommand.Request>(message["payload"]["request"].ToString()
                                    , new JsonSerializerSettings
                                    {
                                        ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                                        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                        TypeNameHandling = TypeNameHandling.All
                                    });
                    
                    var result = _mediator.Send(addOrUpdateRequest).Result;
                    
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private readonly ICache _cache;
        private readonly IMediator _mediator;
    }
}
