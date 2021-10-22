using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace VirtualLibrary.Api.Configurations
{
    public static class AuthenticationJwtConfig
    {
        public static IServiceCollection AddAutheticationWithJwtScheme(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => 
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TonkenKey"])),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
                
            services.AddAuthorization(options => 
            {
                options.AddPolicy("AdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ModeratorRole", policy => policy.RequireRole("Moderador"));
            });
                    
            return services;
        }
    }
}