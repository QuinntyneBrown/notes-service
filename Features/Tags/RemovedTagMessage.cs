using NotesService.Features.Core;
using System;

namespace NotesService.Features.Tags
{
    public class RemovedTagMessage : BaseEventBusMessage
    {
        public RemovedTagMessage(int tagId, Guid correlationId, Guid tenantId)
        {
            Payload = new { Id = tagId, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = TagsEventBusMessages.RemovedTagMessage;        
    }
}
