using Backend;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using WWW.Components;
using WWW.Logging;
using WWW.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.EnableLogging();

string currentDirectory = Directory.GetCurrentDirectory();
string dbPath = Path.Join(currentDirectory, "gqm.db");
string connectionString = $"Data Source={dbPath}";

builder.Services.AddDbContextFactory<DatabaseContext>(opt => opt.UseSqlite(connectionString));

builder.Services.AddScoped<ProjectViewService>();

builder.Services.AddMudServices();

var app = builder.Build();

app.Logger.LogInformation("Database location: {path}", dbPath);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();