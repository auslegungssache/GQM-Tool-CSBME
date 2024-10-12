using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class Project
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string Title { get; set; } = "";
    public List<Goal> Goals { get; set; } = [];
    
}