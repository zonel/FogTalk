namespace FogTalk.Domain.Exceptions;

public class UsernameTakenException : Exception, IBaseException
{
    private readonly int _statusCode = 409;
    public int statusCode => _statusCode;
    public UsernameTakenException(string message) : base(message)
    {
    }
}