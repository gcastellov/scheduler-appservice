namespace Scheduler.Core.Communication
{
    public class AuthorizationResponse
    {
        public bool IsSuccessful { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string At { get; set; }
        public int Failure { get; set; }
    }
}
