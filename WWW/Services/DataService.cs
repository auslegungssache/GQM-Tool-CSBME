using Backend;
using Microsoft.EntityFrameworkCore;

namespace WWW.Services;

public class DataService
{
    private readonly IDbContextFactory<DatabaseContext> dbContextFactory;
    
    DatabaseContext _context;

    public DataService(IDbContextFactory<DatabaseContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
        _context = dbContextFactory.CreateDbContext();
    }
}