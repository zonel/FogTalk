namespace FogTalk.Domain.Shared;

public class Result 
{
    public static Result Success()
    {
        return new Result();
    }
}

public class Result<T>
{
    public bool Succeeded { get; }
    public T Data { get; }
    public string ErrorMessage { get; }

    private Result(bool succeeded, T data, string errorMessage)
    {
        Succeeded = succeeded;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, data, null);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(false, default(T), errorMessage);
    }
}
