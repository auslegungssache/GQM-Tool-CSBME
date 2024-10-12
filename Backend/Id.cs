namespace Backend;

public abstract record Id
{
    public abstract string Value { init; get; }
    
    public static string MakeId()
    {
        return Guid.NewGuid().ToString();
    }
    
    public override string ToString()
    {
        return Value;
    }
}