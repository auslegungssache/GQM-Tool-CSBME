using Backend;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using WWW.Services;

namespace Test;

[TestFixture]
public class ProjectViewServiceTest
{
    private DbContextOptions<DatabaseContext> _dbOpt;
    
    [SetUp]
    public void Setup()
    {
        var dbName = $"GQM-DB_{DateTime.Now.ToFileTimeUtc()}";
        _dbOpt = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        
        using var ctx = new DatabaseContext(_dbOpt);

        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();
    }

    private ProjectViewService CreateService()
    {
        var ctx = new DatabaseContext(_dbOpt);
        
        return new ProjectViewService(ctx);
    }

    [Test]
    public void ServiceAccessible()
    {
        using var service = CreateService();
        
        Assert.That(service.Db, Is.Not.Null);

        {
            Project project = new();
            service.Db.Projects.Add(project);
            
            service.Save();
        }
        
        Assert.That(service.Db.Projects.ToList(), Has.Count.EqualTo(1));
    }

    [Test]
    public void IsDisposable()
    {
        DatabaseContext db;

        {
            using var service = CreateService();
            db = service.Db;
            
            Assert.DoesNotThrow(AccessDatabase);
        }

        Assert.Throws<ObjectDisposedException>(AccessDatabase);
        return;

        void AccessDatabase()
        {
            _ = db.Database;
        }
    }

    [Test]
    public void IsListenable()
    {
        using var service = CreateService();
        var toggle = false;
        
        void Callback(object sender, EventArgs args)
        {
            toggle = true;
        }

        service.ListChanged += Callback;
        {
            Project project = new();
            service.Db.Add(project);
            
            service.Save();
        }
        
        Assert.That(toggle, Is.True);
    }

    [Test]
    public async Task NewAndDelete()
    {
        await using var service = CreateService();

        var project = service.NewProject();
        var goal = service.NewGoal(project);
        var question1 = service.NewQuestion(goal);
        var question2 = service.NewQuestion(goal);
        
        Assert.That(service.Db.Set<Question>().ToList(), Has.Count.EqualTo(2));
        
        var question1Duplicate = await service.GetEntity(question1.Id);
        Assert.That(question1, Is.EqualTo(question1Duplicate));
        
        service.DeleteEntity(question2);
        Assert.That(service.Db.Set<Question>().ToList(), Has.Count.EqualTo(1));
        
        service.DeleteEntity(project);
        Assert.That(service.Db.Set<Goal>().ToList(), Has.Count.EqualTo(0));
    }

    [Test]
    public void SimpleDataRetrieval()
    {
        // this could probably be removed from the class
        
        using var service = CreateService();

        #region project
        var project1 = service.NewProject();
        var project2 = service.NewProject();

        List<Project> expectedProjects = [project1, project2];
        Assert.That(service.Projects, Is.EquivalentTo(expectedProjects));

        service.Db.Remove(project2);
        service.Save();
        expectedProjects = [project1];
        Assert.That(service.Projects, Is.EquivalentTo(expectedProjects));
        
        service.DeleteEntity(project1);
        Assert.That(service.Projects, Is.Empty);
        #endregion
        
        #region user
        var user1 = service.NewUser();
        var user2 = service.NewUser();
        
        List<User> expectedUsers = [user1, user2];
        Assert.That(service.Users, Is.EquivalentTo(expectedUsers));

        service.Db.Remove(user2);
        service.Save();
        expectedUsers = [user1];
        Assert.That(service.Users, Is.EquivalentTo(expectedUsers));
        
        service.DeleteEntity(user1);
        Assert.That(service.Users, Is.Empty);
        #endregion
    }
}