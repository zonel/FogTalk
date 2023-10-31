using FogTalk.Application.Abstraction;
using FogTalk.Application.Security;
using FogTalk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FogTalk.Infrastructure.Persistence;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly IPasswordManager _passwordManager;
    private readonly FogTalkDbContext _dbContext;

    public DatabaseSeeder(FogTalkDbContext dbContext,IPasswordManager passwordManager)
    {
        _passwordManager = passwordManager;
        _dbContext = dbContext;
    }
    
    public void SeedData()
    {
        if (!_dbContext.Users.Any())
        {
            var users = new List<User>
            {
                new User
                {
                    UserName = "user1",
                    Password = _passwordManager.Secure("password1"),
                    Email = "user1@example.com",
                    Bio = "This is the bio for user 1.",
                    ProfilePicture = "profile1.jpg",
                    CreatedAt = DateTime.Now.AddYears(-2),
                },
                new User
                {
                    UserName = "user2",
                    Password = _passwordManager.Secure("password2"),
                    Email = "user2@example.com",
                    Bio = "This is the bio for user 2.",
                    ProfilePicture = "profile2.jpg",
                    CreatedAt = DateTime.Now.AddYears(-1),
                },
                new User
                {
                    UserName = "user3",
                    Password = _passwordManager.Secure("password3"),
                    Email = "user3@example.com",
                    Bio = "This is the bio for user 3.",
                    ProfilePicture = "profile3.jpg",
                    CreatedAt = DateTime.Now.AddMonths(-6),
                }
            };
            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();
            };
        }
    }