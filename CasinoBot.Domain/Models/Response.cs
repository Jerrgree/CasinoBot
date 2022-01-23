namespace CasinoBot.Domain.Models
{
    public class Response
    {
        public bool IsSuccessful { get; set; }

        public string? Message { get; set; }

        public Response()
        {
            IsSuccessful = true;
            Message = null;
        }

        public Response(bool isSuccessful, string? message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
