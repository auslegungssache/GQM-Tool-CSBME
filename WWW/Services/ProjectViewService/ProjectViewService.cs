using Backend;

namespace WWW.Services;

public partial class ProjectViewService() : IDisposable, IAsyncDisposable
{
    public readonly DatabaseContext Db;

    public ProjectViewService(DatabaseContext db) : this()
    {
        Db = db;
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