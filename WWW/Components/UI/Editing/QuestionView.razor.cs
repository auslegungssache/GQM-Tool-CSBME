using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WWW.Services;

namespace WWW.Components.UI;

public partial class QuestionView : ComponentBase
{
    [Inject] protected ProjectViewService ViewService { get; set; } = default!;

    
    [Parameter, EditorRequired] public QuestionId Id { get; set; } = null;
    public Question Question { get; set; } = null;
    

    private bool IsBeingDeleted { get; set; } = false;

    public EditContext EditContext { get; set; } = default!;
    
    protected override void OnInitialized()
    {
        LoadQuestion();
    }

    private async void LoadQuestion()
    {
        Question = await ViewService.GetEntity(Id)
            ?? throw new KeyNotFoundException($"No question found with id {Id}");
        
        EditContext = new EditContext(Question);
    }

    protected override void OnParametersSet()
    {
        IsBeingDeleted = false;
        LoadQuestion();
    }
    
    private void OnSubmit()
    {
        ViewService.Db.SaveChanges();
        EditContext.MarkAsUnmodified();
        ViewService.RefreshView();
    }

    private void DeleteSelf()
    {
        if (IsBeingDeleted) return;
        
        IsBeingDeleted = true;
        StateHasChanged();
        
        ViewService.DeleteEntity(Question);
    }
}