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
    
    [Parameter]
    public Action Refresh { get; set; } = null;

    public List<Goal> Goals()
    {
        var project = Context.Projects
            .Include(p => p.Goals)
            .FirstOrDefault(p => p.Id == Id);
        
        return project?.Goals ?? [];
    }

    public void NewGoal()
    {
        Goal goal = new Goal();
        Context.Find<Project>(Id)?
            .Goals.Add(goal);

        Context.SaveChanges();
    }

    public void Delete()
    {
        var project = Context.Projects.FirstOrDefault(p => p.Id == Id);
        Context.Projects.Remove(project);
        
        Context.SaveChanges();
        Refresh();
    }
}