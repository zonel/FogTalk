namespace FogTalk.Domain.Exceptions;

public class InvalidCredentialsException : Exception, IBaseException
{
    private readonly int _statusCode = 401;
    public int statusCode => _statusCode;
    public InvalidCredentialsException(string message) : base(message)
    {
    }
}