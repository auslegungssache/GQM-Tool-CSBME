namespace Backend.Entities;

public class Project
{
    public ProjectId Id { get; set; } = ProjectId.Make();

    public string Title { get; set; } = "";
    public List<Goal> Goals { get; set; } = [];
    
}

public record ProjectId(string Value) : Id
{
    public static ProjectId Make()
    {
        return new ProjectId(MakeId());
    }
    
    public override string ToString()
    {
        return base.ToString();
    }
}