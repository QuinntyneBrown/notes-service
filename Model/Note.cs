using System;
using System.Collections.Generic;
using NotesService.Data.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static NotesService.Constants;

namespace NotesService.Model
{
    [SoftDelete("IsDeleted")]
    public class Note: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Body { get; set; }

        public ICollection<NoteTag> NoteTags { get; set; } = new HashSet<NoteTag>();

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
