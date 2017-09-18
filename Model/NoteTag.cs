using System;
using System.Collections.Generic;
using NotesService.Data.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static NotesService.Constants;

namespace NotesService.Model
{
    [SoftDelete("IsDeleted")]
    public class NoteTag: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("Note")]
        public int? NoteId { get; set; }

        [ForeignKey("Tag")]
        public int? TagId { get; set; }
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Note Note { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
