using Newtonsoft.Json.Linq;

namespace NotesService.Features.Core
{
    public interface IEventBusMessageHandler
    {
        void Handle(JObject message);
    }
}