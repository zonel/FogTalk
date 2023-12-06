using FogTalk.Application.Security;
using FogTalk.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FogTalk.Infrastructure.Security;


internal sealed class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password) => _passwordHasher.HashPassword(default, password);

    public bool ValidateAsync(string password, string securedPassword, CancellationToken token)
        => _passwordHasher.VerifyHashedPassword(default, securedPassword, password) ==
           PasswordVerificationResult.Success;
}