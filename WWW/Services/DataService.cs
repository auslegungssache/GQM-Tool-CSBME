using Backend;
using Microsoft.EntityFrameworkCore;

namespace WWW.Services;

public class DataService
{
    private readonly IDbContextFactory<DatabaseContext> dbContextFactory;
    
    public DatabaseContext Context;

    public DataService(IDbContextFactory<DatabaseContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
        Context = dbContextFactory.CreateDbContext();
    }
}