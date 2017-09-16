using System;

namespace NotesService.Features.Notes
{
    public class NotesCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Notes] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Notes] GetById {tenantId}-{id}";
    }
}
