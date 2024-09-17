using Backend;
using Backend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace WWW.Components.Pages;

public class Counter : ComponentBase
{
    [Inject]
    protected DatabaseContext Context { get; set; } = default!;


    public List<User> Users()
    {
        return Context.Users
            .ToList();
    }

    public void NewUser()
    {
        User newUser = new User
        {
            Username = "local"
        };

        Context.Users.Add(newUser);
        Context.SaveChanges();
    }
}