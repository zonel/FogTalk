namespace FogTalk.Domain.Exceptions;

public class EmailTakenException : Exception
{
    private readonly int _statusCode = 409;
    public int statusCode => _statusCode;
    public EmailTakenException(string message) : base(message)
    {
    }
}