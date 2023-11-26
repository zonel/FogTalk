namespace FogTalk.Domain.Exceptions;

public class TokenAlreadyBlacklistedException : Exception, IBaseException
{
    private readonly int _statusCode = 401;
    public int statusCode => _statusCode;
    public TokenAlreadyBlacklistedException(string message) : base(message)
    {
    }
}
