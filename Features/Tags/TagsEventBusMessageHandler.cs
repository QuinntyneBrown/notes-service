using NotesService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace NotesService.Features.Tags
{
    public interface ITagsEventBusMessageHandler: IEventBusMessageHandler { }

    public class TagsEventBusMessageHandler: ITagsEventBusMessageHandler
    {
        public TagsEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["type"]}" == TagsEventBusMessages.AddedOrUpdatedTagMessage)
                {
                    _cache.Remove(TagsCacheKeyFactory.Get(new Guid(message["tenantUniqueId"].ToString())));
                }

                if ($"{message["type"]}" == TagsEventBusMessages.RemovedTagMessage)
                {
                    _cache.Remove(TagsCacheKeyFactory.Get(new Guid(message["tenantUniqueId"].ToString())));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private readonly ICache _cache;
    }
}
