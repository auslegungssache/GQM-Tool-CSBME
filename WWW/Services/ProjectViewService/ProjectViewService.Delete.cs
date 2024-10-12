using Backend.Entities;

namespace WWW.Services;

public partial class ProjectViewService
{
    public void DeleteEntity(User user)
    {
        Db.Remove(user);
        Save();
    }
    
    public void DeleteEntity(Project project)
    {
        Db.Remove(project); 
        Save();
    }

    public void DeleteEntity(Goal goal)
    {
        Db.Remove(goal);
        Save();
    }

    public void DeleteEntity(Question question)
    {
        Db.Remove(question);
        Save();
    }
}