using Backend.Entities;

namespace WWW.Services;

public partial class ProjectViewService
{
    public void DeleteEntity(User user)
    {
        Db.Remove(user);
        Logger.LogInformation("""User "{username}" Id "{id}" was deleted""", user.Username, user.Id);
        
        Save();
    }
    
    public void DeleteEntity(Project project)
    {
        Db.Remove(project); 
        Logger.LogInformation("""Project "{title}" Id "{id}" was deleted""", project.Title, project.Id);
        
        Save();
    }

    public void DeleteEntity(Goal goal)
    {
        Db.Remove(goal);
        Logger.LogInformation("""Goal "{title}" Id "{id}" was deleted""", goal.Title, goal.Id);
        
        Save();
    }

    public void DeleteEntity(Question question)
    {
        Db.Remove(question);
        Logger.LogInformation("""Question "{title}" Id "{id}" was deleted""", question.Title, question.Id);
        
        Save();
    }
}