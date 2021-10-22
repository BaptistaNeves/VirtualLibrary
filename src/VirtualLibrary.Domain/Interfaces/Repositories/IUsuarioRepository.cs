using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualLibrary.Domain.DTOs.Usuario;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<UsuarioDto> ObterUsuarioRolesById(Guid Id);
        Task<UsuarioDto> ObterUsuarioRolesByEmail(string email);
        Task<IEnumerable<UsuarioDto>> ObterUsuariosRoles();
    }
}