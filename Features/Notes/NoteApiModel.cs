using NotesService.Model;

namespace NotesService.Features.Notes
{
    public class NoteApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public static TModel FromNote<TModel>(Note note) where
            TModel : NoteApiModel, new()
        {
            var model = new TModel();
            model.Id = note.Id;
            model.TenantId = note.TenantId;
            model.Title = note.Title;
            model.Body = note.Body;
            return model;
        }

        public static NoteApiModel FromNote(Note note)
            => FromNote<NoteApiModel>(note);

    }
}
