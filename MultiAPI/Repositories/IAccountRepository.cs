using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace MultiAPI.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(Register userModel);
        Task<SignInResult> PasswordSignInAsync(Login signInModel);
    }
}
