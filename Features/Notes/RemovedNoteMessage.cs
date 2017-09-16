using NotesService.Features.Core;
using System;

namespace NotesService.Features.Notes
{
    public class RemovedNoteMessage : BaseEventBusMessage
    {
        public RemovedNoteMessage(int noteId, Guid correlationId, Guid tenantId)
        {
            Payload = new { Id = noteId, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = NotesEventBusMessages.RemovedNoteMessage;        
    }
}
