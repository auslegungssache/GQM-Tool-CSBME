using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace WWW.Components.Pages;

public partial class ProjectView : ComponentBase
{
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;

    [Parameter, EditorRequired] public string Id { get; set; } = null;
    
    public Project Project { get; set; }
    
    [Parameter]
    public Action Refresh { get; set; } = null;

    protected override void OnInitialized()
    {
        Project = Context.Set<Project>()
            .Include(p => p.Goals)
            .FirstOrDefault(p => p.Id == Id)!;
    }

    public void OnSubmit()
    {
        Context.SaveChanges();
        Refresh();
    }

    public void NewGoal()
    {
        Goal goal = new();
        Project
            .Goals.Add(goal);

        Context.SaveChanges();
    }

    public void Delete()
    {
        Context.Projects.Remove(Project);
        
        Context.SaveChanges();
        Refresh();
    }
}