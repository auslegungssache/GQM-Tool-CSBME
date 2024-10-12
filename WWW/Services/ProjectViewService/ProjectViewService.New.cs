using Backend.Entities;

namespace WWW.Services;

public partial class ProjectViewService
{
    public User NewUser()
    {
        User user = new User
        {
            Username = "local"
        };
        Db.Users.Add(user);

        Db.SaveChanges();
        RefreshView();

        return user;
    }

    public Project NewProject()
    {
        Project project = new();
        Db.Projects.Add(project);
        Db.SaveChanges();

        RefreshView();

        return project;
    }

    public Goal NewGoal(Project project)
    {
        Goal goal = new();
        project.Goals.Add(goal);

        Db.SaveChanges();
        RefreshView();

        return goal;
    }

    public Question NewQuestion(Goal goal)
    {
        Question question = new();
        goal.Questions.Add(question);

        Db.SaveChanges();
        RefreshView();

        return question;
    }
}