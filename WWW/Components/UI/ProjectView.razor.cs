using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using WWW.Services;

namespace WWW.Components.UI;

public partial class ProjectView : ComponentBase
{
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;
    [Inject] protected ProjectViewService ViewService { get; set; } = default!;

    [Parameter, EditorRequired] public string Id { get; set; } = null;
    
    private bool IsBeingDeleted {get; set;}

    public Project Project { get; set; } = default!;
    public EditContext EditContext { get; set; }
    
    protected override void OnParametersSet()
    {
        IsBeingDeleted = false;
        Load();
    }
    
    protected override void OnInitialized()
    {
        Load();
        ViewService.ListChanged += OnListChanged;
    }

    private async void OnListChanged(object sender, EventArgs e)
    {
        await InvokeAsync(StateHasChanged);
    }
    
    void IDisposable.Dispose()
    {
        ViewService.ListChanged -= OnListChanged;
    }

    private void Load()
    {
        Project = Context.Set<Project>()
            .Include(p => p.Goals)
            .FirstOrDefault(p => p.Id == Id)!;
        
        EditContext = new EditContext(Project);
    }

    public void OnSubmit()
    {
        Context.SaveChanges();
        EditContext.MarkAsUnmodified();
        ViewService.RefreshView();
    }

    public void NewGoal()
    {
        ViewService.NewGoal(Project);
    }

    public void DeleteSelf()
    {
        if (IsBeingDeleted) return;
        IsBeingDeleted = true;
        
        ViewService.DeleteEntity(Project);
    }
}