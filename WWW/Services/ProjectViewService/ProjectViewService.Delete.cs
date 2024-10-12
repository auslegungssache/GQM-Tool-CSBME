using Backend.Entities;

namespace WWW.Services;

public partial class ProjectViewService
{
    public void DeleteEntity(Project project)
    {
        db.Projects.Remove(project);

        db.SaveChanges();
        RefreshView();
    }

    public void DeleteEntity(Goal goal)
    {
        db.Set<Goal>().Remove(goal);

        db.SaveChanges();
        RefreshView();
    }

    public void DeleteEntity(Question question)
    {
        db.Set<Question>().Remove(question);

        db.SaveChanges();
        RefreshView();
    }
}