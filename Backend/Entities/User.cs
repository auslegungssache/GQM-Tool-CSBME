using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public enum Role { Admin, User }

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    
    public string Username { get; set; }
    
    public Role Role { get; set; }
}