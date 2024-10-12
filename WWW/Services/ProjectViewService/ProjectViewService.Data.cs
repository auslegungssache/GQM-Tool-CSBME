using Backend.Entities;

namespace WWW.Services;

public partial class ProjectViewService
{
    public IEnumerable<User> Users => db.Users;
    public IEnumerable<Project> Projects => db.Projects;
}