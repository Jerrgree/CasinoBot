namespace CasinoBot.Domain.Models
{
    public class Response<T> : Response
    {
        public T Value { get; set; }

        public Response(T value) : base()
        {
            Value = value;
        }

        public Response(bool isSuccessful, string? message, T value) : base(isSuccessful, message)
        {
            Value = value;
        }
    }
}
