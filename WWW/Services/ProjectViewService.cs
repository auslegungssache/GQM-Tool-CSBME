using Backend;
using Backend.Entities;

namespace WWW.Services;

public class ProjectViewService(DatabaseContext dbContext, DataService data)
{
    DatabaseContext _dbContext = dbContext;
    DataService _data = data;
    
    public IEnumerable<User> Users => _dbContext.Users;
    public IEnumerable<Project> Projects => _dbContext.Projects;
    
    public event EventHandler<EventArgs> ListChanged = delegate { };
    protected void NotifyListChanged(object sender, EventArgs e)
        => ListChanged.Invoke(sender, e);


    public void RefreshView()
    {
        NotifyListChanged(Projects, EventArgs.Empty);
    }

    public User NewUser()
    {
        User user = new User
        {
            Username = "local"
        };
        _dbContext.Users.Add(user);
        
        _dbContext.SaveChanges();
        RefreshView();

        return user;
    }
    
    public Project NewProject()
    {
        Project project  = new();
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
        
        RefreshView();
        
        return project;
    }
    
    public Goal NewGoal(Project project)
    {
        Goal goal = new();
        project.Goals.Add(goal);

        _dbContext.SaveChanges();
        RefreshView();
        
        return goal;
    }
    
    public Question NewQuestion(Goal goal)
    {
        Question question = new();
        goal.Questions.Add(question);

        _dbContext.SaveChanges();
        RefreshView();

        return question;
    }

    public void DeleteEntity(Project project)
    {
        _dbContext.Projects.Remove(project);
        
        _dbContext.SaveChanges();
        RefreshView();
    }
    public void DeleteEntity(Goal goal)
    {
        _dbContext.Set<Goal>().Remove(goal);
        
        _dbContext.SaveChanges();
        RefreshView();
    }
    public void DeleteEntity(Question question)
    {
        _dbContext.Set<Question>().Remove(question);

        _dbContext.SaveChanges();
        RefreshView();
    }
}