using NotesService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace NotesService.Features.Notes
{
    public interface INotesEventBusMessageHandler: IEventBusMessageHandler { }

    public class NotesEventBusMessageHandler: INotesEventBusMessageHandler
    {
        public NotesEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
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
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private readonly ICache _cache;
    }
}
