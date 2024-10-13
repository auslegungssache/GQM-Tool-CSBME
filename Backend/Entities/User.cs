using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public enum Role { Admin, User }

public class User : CoreEntity
{
    public UserId Id { get; set; } = UserId.Make();
    
    [Column("Username")]
    public override string Title { get; set; }
    
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