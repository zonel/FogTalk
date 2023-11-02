namespace FogTalk.Domain.Exceptions;

public class IdempotencyException : Exception
{

        public IdempotencyException()
        {
        }

        public IdempotencyException(string message) : base(message)
        {
        }

        public IdempotencyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
