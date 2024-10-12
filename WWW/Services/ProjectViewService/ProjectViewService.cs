using Backend;

namespace WWW.Services;

public partial class ProjectViewService()
{
    public readonly DatabaseContext Db;
    public readonly DataService Data;

    public ProjectViewService(DatabaseContext db, DataService data) : this()
    {
        Db = db;
        Data = data;
    }
    
    public event EventHandler<EventArgs> ListChanged = delegate { };
    protected void NotifyListChanged(object sender, EventArgs e)
        => ListChanged.Invoke(sender, e);


    public void RefreshView()
    {
        NotifyListChanged(Projects, EventArgs.Empty);
    }
}