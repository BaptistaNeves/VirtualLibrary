using Microsoft.Extensions.DependencyInjection;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Interfaces.Services;
using VirtualLibrary.Business.Notifications;
using VirtualLibrary.Business.Services;
using VirtualLibrary.Data.Context;
using VirtualLibrary.Data.Repositories;
using VirtualLibrary.Domain.Interfaces.Services.Token;
using VirtualLibrary.Api.Services;

namespace VirtualLibrary.Api.Configurations
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IEditoraRepository, EditoraRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            //services.AddScoped<IAluguerRepository, AluguerRepository>();

            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IEditoraService, EditoraService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ILivroService, LivroService>();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}