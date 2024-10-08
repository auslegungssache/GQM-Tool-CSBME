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
    
    public List<Question> Questions()
    {
        var goal = Context
            .Set<Goal>()
            .Include(g => g.Questions)
            .FirstOrDefault(g => g.Id == Id);
        
        return goal?.Questions ?? [];
    }
    
    public void NewQuestion()
    {
        Question question = new();
        Context.Find<Goal>(Id)?
            .Questions.Add(question);

        Context.SaveChanges();
    }

    public void Delete()
    {
        var goal = Context.Find<Goal>(Id);
        Context.Set<Goal>()
            .Remove(goal);

        Context.SaveChanges();
        Refresh();
    }
}