﻿using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using WWW.Services;

namespace WWW.Components.UI;

public partial class GoalView : ComponentBase
{
    [Inject] protected ProjectViewService ViewService { get; set; } = default!;
    
    [Parameter, EditorRequired] public GoalId Id { get; set; } = null;
    
    private bool IsBeingDeleted { get; set; }
    
    public EditContext EditContext { get; set; } = default!;

    public Goal Goal { get; set; } = default!;
    
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

    protected async void Load()
    {
        Goal = await ViewService.GetEntity(Id)
               ?? throw new KeyNotFoundException($"No goal found with id {Id}");
        
        EditContext = new EditContext(Goal);
    }

    public void NewQuestion()
    {
        ViewService.NewQuestion(Goal);
    }

    public void DeleteSelf()
    {
        if (IsBeingDeleted) return;
        
        IsBeingDeleted = true;
        StateHasChanged();
        
        ViewService.DeleteEntity(Goal);
    }

    public void OnSubmit()
    {
        ViewService.Db.SaveChanges();
        EditContext.MarkAsUnmodified();
        ViewService.RefreshView();
    }
}