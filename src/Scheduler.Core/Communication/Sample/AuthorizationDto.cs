namespace Scheduler.Core.Communication.Sample
{
    public class AuthorizationDto
    {
        public bool IsSuccessful { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string At { get; set; }
        public int Failure { get; set; }
    }
}
