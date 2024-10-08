using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;

namespace WWW.Components.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;

    protected List<Project> Projects { get; set; } = [];

    protected override void OnInitialized()
    {
        Projects = Context.Projects
            .ToList();
    }


    public List<User> Users()
    {
        return Context.Users
            .ToList();
    }

    public void NewProject()
    {
        Project project  = new();
        Context.Projects.Add(project);
        Context.SaveChanges();
        
        Refresh();
    }

    public void NewUser()
    {
        User newUser = new User
        {
            Username = "local"
        };

        Context.Users.Add(newUser);
        Context.SaveChanges();
    }

    public void Refresh()
    {
        Projects = Context.Projects.ToList();
        StateHasChanged();
    }
}