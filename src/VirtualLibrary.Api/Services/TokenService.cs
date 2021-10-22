using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VirtualLibrary.Domain.DTOs.Login;
using VirtualLibrary.Domain.Interfaces.Services.Token;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Api.Services
{
    //We must install this package System.IdentityModel.Tokens.Jwt for create our token
    public class TokenService : ITokenService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config,
                            UserManager<Usuario> userManager)
        {
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TonkenKey"]));
        }

        public async Task<string> CreateToken(string email)
        {
            var usuario  = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(usuario);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, usuario.UserName)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenHandler =  new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}