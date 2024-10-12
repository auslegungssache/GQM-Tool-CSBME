using Backend;

namespace WWW.Services;

public partial class ProjectViewService() : IDisposable, IAsyncDisposable
{
    public readonly DatabaseContext Db;
    public readonly DataService Data;

    public ProjectViewService(DatabaseContext db, DataService data) : this()
    {
        Db = db;
        Data = data;
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