using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace WWW.Components.Pages;

public partial class GoalView : ComponentBase
{
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;
    
    [Parameter, EditorRequired] public string Id { get; set; } = null;
    
    [Parameter, EditorRequired]
    public Action Refresh { get; set; } = null;
    public EditContext EditContext { get; set; } = default!;
    
    public Goal Goal { get; set; }
    protected override void OnInitialized()
    {
        Goal = Context.Set<Goal>()
            .Find(Id)!;
        
        EditContext = new EditContext(Goal);
    }

    public void OnSubmit()
    {
        Context.SaveChanges();
        EditContext.MarkAsUnmodified();
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