using NotesService.Model;

namespace NotesService.Features.Tags
{
    public class TagApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromTag<TModel>(Tag tag) where
            TModel : TagApiModel, new()
        {
            var model = new TModel();
            model.Id = tag.Id;
            model.TenantId = tag.TenantId;
            model.Name = tag.Name;
            return model;
        }

        public static TagApiModel FromTag(Tag tag)
            => FromTag<TagApiModel>(tag);

    }
}
