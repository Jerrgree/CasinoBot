using CasinoBot.Domain.Enums;

namespace CasinoBot.Domain.Models
{
    public class Response
    {
        public bool IsSuccessful { get; set; }

        public ResponseCode ResponseCode { get; set; }

        public Response()
        {
            IsSuccessful = true;
            ResponseCode = ResponseCode.Success;
        }

        public Response(bool isSuccessful, ResponseCode responseCode)
        {
            IsSuccessful = isSuccessful;
            ResponseCode = responseCode;
        }
    }
}
