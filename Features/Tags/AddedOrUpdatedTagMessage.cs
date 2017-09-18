using NotesService.Model;
using NotesService.Features.Core;
using System;

namespace NotesService.Features.Tags
{

    public class AddedOrUpdatedTagMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedTagMessage(Tag tag, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = tag, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = TagsEventBusMessages.AddedOrUpdatedTagMessage;        
    }
}
