using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace WWW.Components.Pages;

public partial class QuestionView : ComponentBase
{
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;
    
    [Parameter, EditorRequired] public string Id { get; set; } = null;
    
    [Parameter]
    public Action Refresh { get; set; } = null;

    public void Delete()
    {
        var question = Context.Set<Question>().Find(Id);
        Context.Set<Question>()
            .Remove(question);

        Context.SaveChanges();
        Refresh();
    }
}