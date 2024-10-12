using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using WWW.Services;

namespace WWW.Components.UI;

public partial class ProjectView : ComponentBase
{
    [Inject] protected ProjectViewService ViewService { get; set; } = default!;

    [Parameter, EditorRequired] public ProjectId Id { get; set; } = null;
    
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

    private async void Load()
    {
        Project = await ViewService.GetEntity(Id)
            ?? throw new KeyNotFoundException($"No project found with id {Id}");
        
        EditContext = new EditContext(Project);
    }

}