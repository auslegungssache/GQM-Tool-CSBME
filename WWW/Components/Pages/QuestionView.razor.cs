using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace WWW.Components.Pages;

public partial class QuestionView : ComponentBase
{
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;
    
    [Parameter, EditorRequired] public string Id { get; set; } = null;
    
    [Parameter]
    public Action Refresh { get; set; } = null;
    
    public Question Question { get; set; } = null;
    public EditContext EditContext { get; set; } = default!;
    
    protected override void OnInitialized()
    {
        Question = Context.Set<Question>()
            .Find(Id)!;
        
        EditContext = new EditContext(Question);
    }

    protected void OnSubmit()
    {
        Context.SaveChanges();
        EditContext.MarkAsUnmodified();
        Refresh?.Invoke();
    }

    public void Delete()
    {
        Context.Set<Question>()
            .Remove(Question);

        Context.SaveChanges();
        Refresh();
    }
}