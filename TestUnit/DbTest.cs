using Backend;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Test;

[TestFixture]
public class DbTest
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

    private DatabaseContext CreateContext()
    {
        var ctx = new DatabaseContext(_dbOpt);

        return ctx;
    }
    
    [Test]
    public void CreateProject()
    {
        using var ctx = CreateContext();

        Assert.That(ctx.Projects.Count(), Is.EqualTo(0));

        var project = new Project();
        ctx.Projects.Add(project);
        ctx.SaveChanges();
        
        Assert.Multiple(() =>
        {
            Assert.That(ctx.Projects.ToList(), Does.Contain(project));
            Assert.That(ctx.Projects.Count(), Is.EqualTo(1));
        });
    }

    [Test]
    public void CreateGoal()
    {
        using var ctx = CreateContext();
        
        var project = new Project();
        ctx.Projects.Add(project);
        
        // add goal by directly adding it to the project
        var goalDirect = new Goal();
        project.Goals.Add(goalDirect);
        
        // add goal by attaching a project to it and attaching it to the context
        var goalIndirect = new Goal
        {
            Project = project
        };
        ctx.Attach(goalIndirect);
        
        ctx.SaveChanges();
        
        Assert.Multiple(() =>
        {
            Assert.That(ctx.Projects.ToList(), Does.Contain(project));
            
            Assert.That(goalDirect.Id.ToString(), Is.Not.Empty);
            Assert.That(goalIndirect.Id.ToString(), Is.Not.Empty);
            
            Assert.That(project.Goals, Does.Contain(goalIndirect));
            Assert.That(project.Goals, Does.Contain(goalDirect));
        });
    }

    [Test]
    public void CreateQuestion()
    {
        using var ctx = CreateContext();
        
        var project = new Project();
        var goal = new Goal();
        var question = new Question
        {
            Title = "foobar"
        };
        
        project.Goals.Add(goal);
        goal.Questions.Add(question);
        
        ctx.Projects.Add(project);

        ctx.SaveChanges();

        var ctxQuestion = ctx.Projects
            .Include(project => project.Goals)
            .ThenInclude(goal => goal.Questions)
            
            .Single()
            .Goals.Single()
            .Questions.Single();
        Assert.Multiple(() =>
        {
            Assert.That(ctxQuestion.Title, Is.EqualTo(question.Title));
            Assert.That(ctxQuestion.Id.ToString(), Is.Not.Empty);
        });
    }

    [Test]
    public void CascadeDelete()
    {
        using var ctx = CreateContext();
        
        var project = new Project();

        {
            var goal1 = new Goal();
            {
                var question1 = new Question();
                var question2 = new Question();
            
                goal1.Questions.AddRange([question1, question2]);
            }
            var goal2 = new Goal();
            {
                var question1 = new Question();
                var question2 = new Question();
            
                goal2.Questions.AddRange([question1, question2]);
            }
        
            project.Goals.AddRange([goal1, goal2]);
        }
        ctx.Projects.Add(project);
        
        ctx.SaveChanges();

        {
            var questions = ctx.Set<Question>().ToList();
            Assert.That(questions, Has.Count.EqualTo(4));
            var goals = ctx.Set<Goal>().ToList();
            Assert.That(goals, Has.Count.EqualTo(2));
        }
        
        ctx.Projects.Remove(ctx.Projects.Single());
        ctx.SaveChanges();
        
        Assert.Multiple(() =>
        {
            Assert.That(ctx.Projects, Is.Empty);
            Assert.That(ctx.Set<Goal>(), Is.Empty);
            Assert.That(ctx.Set<Question>(), Is.Empty);
        });
    }
}