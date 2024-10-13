using Microsoft.AspNetCore.Components;
using WWW.Services;

namespace WWW.Components.UI;

public partial class Drawer : ComponentBase, IDisposable
{
    [Inject]
    public ProjectViewService ViewService { get; set; } = null!;
    
    protected override void OnInitialized()
    {
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
}