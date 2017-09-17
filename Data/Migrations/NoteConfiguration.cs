using System.Data.Entity.Migrations;
using NotesService.Data;

namespace NotesService.Data.Migrations
{
    public class NoteConfiguration
    {
        public static void Seed(NotesServiceContext context) {

            foreach(var note in context.Notes) {
                note.IsDeleted = true;
            }
            context.SaveChanges();
        }
    }
}
