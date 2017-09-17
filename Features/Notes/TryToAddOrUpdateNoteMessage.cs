using NotesService.Model;
using NotesService.Features.Core;
using System;

namespace NotesService.Features.Notes
{

    public class TryToAddOrUpdateNoteMessage : BaseEventBusMessage
    {
        public TryToAddOrUpdateNoteMessage(AddOrUpdateNoteCommand.Request request, Guid correlationId, Guid tenantId, string username)
        {
            request.Username = username;
            request.TenantUniqueId = tenantId;
            request.CorrelationId = correlationId;
            Payload = new { Request = request, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = NotesEventBusMessages.TryToAddOrUpdateNoteMessage;
    }
}
