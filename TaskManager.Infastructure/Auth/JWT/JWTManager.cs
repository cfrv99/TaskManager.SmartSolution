using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;
using TaskManager.Infastructure.Constants;

namespace TaskManager.Infastructure.Auth.JWT
{
    public interface IJwtManager
    {
        Task<AccessTokenModel> GenerateAccessToken(AppUser appUser, List<string> roles);
    }
    public class JwtManager : IJwtManager
    {
        private readonly TokenConfigurationOptions tokenOptions;

        public JwtManager(IOptions<TokenConfigurationOptions> tokenOptions)
        {
            this.tokenOptions = tokenOptions.Value;
        }
        public async Task<AccessTokenModel> GenerateAccessToken(AppUser appUser, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(ClaimTypes.Email,appUser.Email),
                new Claim("Tenant",appUser.OrganizationId.ToString())
            };

            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.AddRange(roleClaims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Key));
            var token = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: claims.ToArray(),
                notBefore: DateTime.Now,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(tokenOptions.ExpiredTime)),
                signingCredentials: new SigningCredentials(
                    securityKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new AccessTokenModel
            {
                AccessToken = encodedToken,
                Expired = DateTime.Now + TimeSpan.FromMinutes(tokenOptions.ExpiredTime)
            };
        }
    }
}
