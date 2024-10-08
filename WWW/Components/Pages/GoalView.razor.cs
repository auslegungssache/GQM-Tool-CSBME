using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace WWW.Components.Pages;

public partial class GoalView : ComponentBase
{
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;
    
    [Parameter, EditorRequired] public string Id { get; set; } = null;
    
    [Parameter]
    public Action Refresh { get; set; } = null;
    
    public Goal Goal { get; set; }
    protected override void OnInitialized()
    {
        Goal = Context.Set<Goal>()
            .Find(Id);
    }

    public void OnSubmit()
    {
        Context.SaveChanges();
        Refresh();
    }
    
    public void NewQuestion()
    {
        Question question = new();
        Goal.Questions.Add(question);

        Context.SaveChanges();
    }

    public void Delete()
    {
        Context.Set<Goal>().Remove(Goal);

        Context.SaveChanges();
        Refresh();
    }
}