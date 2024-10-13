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
        Logger.LogInformation("""User "{username}" Id "{id}" was created""", user.Username, user.Id);

        Save();

        return user;
    }

    public Project NewProject()
    {
        Project project = new();
        Db.Projects.Add(project);
        Logger.LogInformation("""Project "{title}" Id "{id}" was created""",project.Title, project.Id);

        Save();
        
        return project;
    }

    public Goal NewGoal(Project project)
    {
        Goal goal = new();
        project.Goals.Add(goal);
        Logger.LogInformation(
            """Goal "{title}" Id "{id}", attached to project "{projectTitle}" Id "{projectId}", was created""",
            goal.Title, goal.Id,
            project.Title, project.Id);

        Save();

        return goal;
    }

    public Question NewQuestion(Goal goal)
    {
        Question question = new();
        goal.Questions.Add(question);
        Logger.LogInformation(
            """Question "{title}" Id "{id}", attached to goal "{goalTitle}" Id "{goalId}", was created""",
            question.Title, question.Id,
            goal.Title, goal.Id);

        Save();

        return question;
    }
}