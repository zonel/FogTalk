namespace FogTalk.Domain.Shared;


public class Result<TValue>
{
    public Result(TValue value)
    {
        Value = value;
    }

    public TValue Value { get; }
}

public class Result 
{
    public static Result Success()
    {
        return new Result();
    }
}
