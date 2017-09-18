using System;

namespace NotesService.Features.Tags
{
    public class TagsCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Tags] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Tags] GetById {tenantId}-{id}";
    }
}
