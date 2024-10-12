namespace WWW.Services;

public partial class ProjectViewService
{
    public event EventHandler<EventArgs> ListChanged = delegate { };

    protected void NotifyListChanged(object sender, EventArgs e)
        => ListChanged.Invoke(sender, e);

    public void RefreshView()
    {
        NotifyListChanged(Projects, EventArgs.Empty);
    }
}