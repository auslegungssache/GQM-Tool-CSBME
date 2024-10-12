using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace WWW.Services;

public partial class ProjectViewService
{
    public IEnumerable<User> Users => db.Users;
    public IEnumerable<Project> Projects => db.Projects;

    public async Task<User?> GetEntity(UserId id)
    {
        return await db.Users
            .SingleOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<Project?> GetEntity(ProjectId id)
    {
        return await db.Projects
            .Include(p => p.Goals)
            .ThenInclude(g => g.Questions)
            .SingleOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Goal?> GetEntity(GoalId id)
    {
        return await db.Set<Goal>()
            .Include(q => q.Questions)
            .SingleOrDefaultAsync(q => q.Id == id);
    }
    
    public async Task<Question?> GetEntity(QuestionId id)
    {
        return await db.Set<Question>()
            .SingleOrDefaultAsync(q => q.Id == id);
    }

}