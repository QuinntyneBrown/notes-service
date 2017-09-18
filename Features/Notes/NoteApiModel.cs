using NotesService.Features.Tags;
using NotesService.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NotesService.Features.Notes
{
    public class NoteApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }        
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public ICollection<TagApiModel> Tags = new HashSet<TagApiModel>();

        public static TModel FromNote<TModel>(Note note) where
            TModel : NoteApiModel, new()
        {
            var model = new TModel();
            model.Id = note.Id;
            model.TenantId = note.TenantId;
            model.Title = note.Title;
            model.Body = note.Body;
            model.Slug = note.Slug;
            model.Tags = note.NoteTags.Select(x => TagApiModel.FromTag(x.Tag)).ToList();
            return model;
        }

        public static NoteApiModel FromNote(Note note)
            => FromNote<NoteApiModel>(note);

    }
}
