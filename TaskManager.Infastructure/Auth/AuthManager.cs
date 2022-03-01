using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;
using TaskManager.Infastructure.Auth.JWT;
using TaskManager.Infastructure.Auth.Models;

namespace TaskManager.Infastructure.Auth
{
    public interface IAuthManager
    {
        Task<LoginResult> Login(string email, string password);
        Task<SignUpResult> SignUp(SignUpRequest request, bool isAdmin, int organisationId);
    }
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IJwtManager _jwtManager;

        public AuthManager(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IJwtManager jwtManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtManager = jwtManager;
        }
        public async Task<LoginResult> Login(string email, string password)
        {
            var loggedUser = await _userManager.FindByEmailAsync(email);
            if (loggedUser != null)
            {
                var passwordResult = await _userManager.CheckPasswordAsync(loggedUser, password);
                if (passwordResult)
                {
                    var userRoles = (await _userManager.GetRolesAsync(loggedUser)).ToList();
                    var accessToken = await _jwtManager.GenerateAccessToken(loggedUser, userRoles);
                    return new LoginResult { AccessToken = accessToken.AccessToken, Expired = accessToken.Expired };
                }
                else
                {
                    throw new Exception("UserName or password is not correct");
                }
            }
            else
            {
                throw new Exception("email not founded");
            }
        }

        public async Task<SignUpResult> SignUp(SignUpRequest request, bool isAdmin, int organisationId)
        {
            var userIsExist = await _userManager.FindByEmailAsync(request.Email);
            if (userIsExist == null)
            {
                var user = new AppUser
                {
                    Email = request.Email,
                    UserName = request.UserName,
                    OrganizationId = organisationId
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (isAdmin)
                {
                    var adminRole = await _roleManager.FindByNameAsync("Admin");
                    if (adminRole == null)
                    {
                        var role = new AppRole() { Name = "Admin" };

                        var roleCreateResult = await _roleManager.CreateAsync(role);

                        if (!roleCreateResult.Succeeded)
                            throw new Exception();
                    }

                    var signedUser = await _userManager.FindByEmailAsync(request.Email);
                    await _userManager.AddToRoleAsync(signedUser, "Admin");
                }
                if (result.Succeeded)
                {
                    return new SignUpResult { };
                }
                else
                {
                    throw new Exception("Password is not on correct type");
                }
            }
            else
            {
                throw new Exception("this user already exist");
            }
        }
    }
}
