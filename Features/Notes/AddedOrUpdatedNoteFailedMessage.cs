using NotesService.Model;
using NotesService.Features.Core;
using System;

namespace NotesService.Features.Notes
{

    public class AddedOrUpdatedNoteFailedMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedNoteFailedMessage(Guid correlationId, Guid tenantId)
        {
            Payload = new { CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = NotesEventBusMessages.NoteAddedOrUpdatedFailedMessage;
    }
}
