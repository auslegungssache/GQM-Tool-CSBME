using Backend.Entities;

namespace WWW.Services;

public partial class ProjectViewService
{
    public void DeleteEntity(Project project)
    {
        Db.Projects.Remove(project);

        Db.SaveChanges();
        RefreshView();
    }

    public void DeleteEntity(Goal goal)
    {
        Db.Set<Goal>().Remove(goal);

        Db.SaveChanges();
        RefreshView();
    }

    public void DeleteEntity(Question question)
    {
        Db.Set<Question>().Remove(question);

        Db.SaveChanges();
        RefreshView();
    }
}