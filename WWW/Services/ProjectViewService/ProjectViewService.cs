using Backend;

namespace WWW.Services;

public partial class ProjectViewService() : IDisposable, IAsyncDisposable
{
    public readonly DatabaseContext Db;
    public readonly ILogger Logger;

    public ProjectViewService(DatabaseContext db, ILogger<ProjectViewService> logger) : this()
    {
        Db = db;
        Logger = logger;
    }

    public async ValueTask DisposeAsync()
    {
        await Db.DisposeAsync();
    }
    
    public void Dispose()
    {
        Db.Dispose();
    }
}