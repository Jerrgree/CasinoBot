using CasinoBot.Domain.Enums;

namespace CasinoBot.Domain.Models
{
    public class Response<T> : Response
    {
        public T Value { get; set; }

        public Response(T value) : base()
        {
            Value = value;
        }

        public Response(bool isSuccessful, ResponseCode responseCode, T value) : base(isSuccessful, responseCode)
        {
            Value = value;
        }
    }
}
