using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class Goal
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public List<Question> Questions { get; set; } = [];
}