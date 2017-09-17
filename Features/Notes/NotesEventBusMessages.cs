namespace NotesService.Features.Notes
{
    public class NotesEventBusMessages
    {
        public static string TryToAddOrUpdateNoteMessage = "[Notes] TryToAddOrUpdateNote";
        public static string NoteAddedOrUpdatedFailedMessage = "[Notes] NoteAddedOrUpdatedFailed";
        public static string AddedOrUpdatedNoteMessage = "[Notes] NoteAddedOrUpdated";
        public static string RemovedNoteMessage = "[Notes] NoteRemoved";
    }
}
