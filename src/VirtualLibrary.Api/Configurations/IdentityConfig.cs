using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VirtualLibrary.Api.Extensions;
using VirtualLibrary.Data.Context;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Api.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
        {
             services.AddIdentityCore<Usuario>(opt => {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<Usuario>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<Usuario>>(TokenOptions.DefaultProvider)
                .AddErrorDescriber<IdentityMensagensPortugues>();

            return services;
        }
    }
}