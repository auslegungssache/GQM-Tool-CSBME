using Backend;
using Microsoft.AspNetCore.Components;
using WWW.Services;

namespace WWW.Components.Pages;

public partial class Index : ComponentBase
{
    [Inject] protected ProjectViewService ViewService { get; set; } = default!;
    
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;

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