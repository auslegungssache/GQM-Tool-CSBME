using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using WWW.Services;

namespace WWW.Components.UI;

public partial class QuestionView : ComponentBase
{
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;
    [Inject] protected ProjectViewService ViewService { get; set; } = default!;

    
    [Parameter, EditorRequired] public string Id { get; set; } = null;
    public Question Question { get; set; } = null;
    

    private bool IsBeingDeleted { get; set; } = false;

    public EditContext EditContext { get; set; } = default!;
    
    protected override void OnInitialized()
    {
        LoadQuestion();
    }

    private void LoadQuestion()
    {
        Question = Context.Set<Question>()
            .Find(Id)!;
        
        EditContext = new EditContext(Question);
    }

    protected override void OnParametersSet()
    {
        IsBeingDeleted = false;
        LoadQuestion();
    }
    
    private void OnSubmit()
    {
        Context.SaveChanges();
        EditContext.MarkAsUnmodified();
        ViewService.RefreshView();
    }

    private void Delete()
    {
        if (IsBeingDeleted) return;
        
        IsBeingDeleted = true;
        StateHasChanged();
        
        ViewService.DeleteEntity(Question);
    }
}