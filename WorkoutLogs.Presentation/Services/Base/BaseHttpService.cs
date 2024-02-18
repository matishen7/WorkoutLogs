namespace WorkoutLogs.Presentation.Services.Base
{
    public class BaseHttpService
    {
        public IClient _client;
        public BaseHttpService(IClient client) { _client = client; }
    }
}