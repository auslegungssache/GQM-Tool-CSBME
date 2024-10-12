using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class Goal
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string Title { get; set; } = "";
    public GoalPriority Priority { get; set; } = GoalPriority.Medium;
    public List<Question> Questions { get; set; } = [];
}

public enum GoalPriority { Low, Medium, High }