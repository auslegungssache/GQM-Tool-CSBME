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
        db.Users.Add(user);

        db.SaveChanges();
        RefreshView();

        return user;
    }

    public Project NewProject()
    {
        Project project = new();
        db.Projects.Add(project);
        db.SaveChanges();

        RefreshView();

        return project;
    }

    public Goal NewGoal(Project project)
    {
        Goal goal = new();
        project.Goals.Add(goal);

        db.SaveChanges();
        RefreshView();

        return goal;
    }

    public Question NewQuestion(Goal goal)
    {
        Question question = new();
        goal.Questions.Add(question);

        db.SaveChanges();
        RefreshView();

        return question;
    }
}