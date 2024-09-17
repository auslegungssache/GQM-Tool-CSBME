using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class Question
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    
    public string Title { get; set; }
    public string Metric { get; set; }
    
    public bool Done { get; set; }
}