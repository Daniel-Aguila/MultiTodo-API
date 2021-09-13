using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiAPI.Models;
using MultiAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MultiAPI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(IAccountRepository accountRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(Register userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);
                if (!result.Succeeded)
                {
                    foreach(var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                }
                ModelState.Clear();
            }
            return Ok();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]Login model)
        {
            if (ModelState.IsValid)
            {
               var result = await _accountRepository.PasswordSignInAsync(model);
               if (result.Succeeded)
               {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstantsModel.Key));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, model.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    var token = new JwtSecurityToken(
                        JwtConstantsModel.Issuer,
                        JwtConstantsModel.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: creds
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                    });
                }
            }
             return BadRequest(ModelState);
        }

    }
}
