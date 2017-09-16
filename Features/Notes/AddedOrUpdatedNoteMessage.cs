using NotesService.Model;
using NotesService.Features.Core;
using System;

namespace NotesService.Features.Notes
{

    public class AddedOrUpdatedNoteMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedNoteMessage(Note note, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = note, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = NotesEventBusMessages.AddedOrUpdatedNoteMessage;        
    }
}
