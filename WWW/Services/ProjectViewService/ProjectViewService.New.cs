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

        Save();

        return user;
    }

    public Project NewProject()
    {
        Project project = new();
        Db.Projects.Add(project);

        Save();
        
        return project;
    }

    public Goal NewGoal(Project project)
    {
        Goal goal = new();
        project.Goals.Add(goal);

        Save();

        return goal;
    }

    public Question NewQuestion(Goal goal)
    {
        Question question = new();
        goal.Questions.Add(question);

        Save();

        return question;
    }
}