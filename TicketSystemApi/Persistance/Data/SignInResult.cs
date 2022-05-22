using System;

namespace TicketSystemApi.Persistance.Data
{
    public class SignIn_Result
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExprirationDate { get; set; }
        public string ID { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public SignIn_Result()
        {
            Success = false;
        }
    }
}
