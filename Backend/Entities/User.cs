namespace Backend.Entities;

public enum Role { Admin, User }

public class User
{
    public UserId Id { get; set; } = UserId.Make();
    
    public string Username { get; set; }
    
    public Role Role { get; set; }
}

public record UserId(string Value) : Id
{
    public static UserId Make()
    {
        return new UserId(MakeId());
    }
    
    public override string ToString()
    {
        return base.ToString();
    }
}