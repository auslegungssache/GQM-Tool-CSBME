using Backend.Entities;

namespace WWW.Services;

public partial class ProjectViewService
{
    private string NewName<T>(string defaultName) where T : CoreEntity
    {
        int i = 0;
        while (true)
        {
            var nameSuffix = i != 0 ? $" ({i})" : string.Empty;
            var name = $"{defaultName}{nameSuffix}";
            if (Db.Set<T>().Any(e => e.Title == name))
            {
                i++;
            }
            else
            {
                return name;
            }

        }
    }
    
    public User NewUser()
    {
        User user = new ()
        {
            Title = NewName<User>("New User")
        };
        Db.Users.Add(user);
        Logger.LogInformation("""User "{username}" Id "{id}" was created""", user.Title, user.Id);

        Save();

        return user;
    }

    public Project NewProject()
    {
        Project project = new()
        {
            Title = NewName<Project>("New Project")
        };
        Db.Projects.Add(project);
        Logger.LogInformation("""Project "{title}" Id "{id}" was created""",project.Title, project.Id);

        Save();
        
        return project;
    }

    public Goal NewGoal(Project project)
    {
        Goal goal = new()
        {
            Title = NewName<Goal>("New Goal")
        };
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
        Question question = new()
        {
            Title = NewName<Question>("New Question")
        };
        goal.Questions.Add(question);
        Logger.LogInformation(
            """Question "{title}" Id "{id}", attached to goal "{goalTitle}" Id "{goalId}", was created""",
            question.Title, question.Id,
            goal.Title, goal.Id);

        Save();

        return question;
    }
}