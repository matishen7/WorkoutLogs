namespace WorkoutLogs.Presentation.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; set; }
    }
}
