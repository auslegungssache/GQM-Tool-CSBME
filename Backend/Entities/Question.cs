namespace Backend.Entities;

public class Question : CoreEntity
{
    public QuestionId Id { get; set; } = QuestionId.Make();
    public Goal Goal { get; set; }

    public override string Title { get; set; } = "";
    public string Metric { get; set; } = "";

    public bool Done { get; set; } = false;
}

public record QuestionId(string Value) : Id
{
    public static QuestionId Make()
    {
        return new QuestionId(MakeId());
    }
    
    public override string ToString()
    {
        return base.ToString();
    }
}