namespace Backend.Entities;

public class Goal : CoreEntity
{
    public GoalId Id { get; set; } = GoalId.Make();
    public Project Project { get; set; }

    public override string Title { get; set; } = "";
    public GoalPriority Priority { get; set; } = GoalPriority.Medium;
    public List<Question> Questions { get; set; } = [];
}

public record GoalId(string Value) : Id
{
    public static GoalId Make()
    {
        return new GoalId(MakeId());
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public enum GoalPriority { Low, Medium, High }