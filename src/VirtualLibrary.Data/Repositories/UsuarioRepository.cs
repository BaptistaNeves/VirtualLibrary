using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Data.Context;
using VirtualLibrary.Domain.DTOs.Usuario;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext db) : base(db){}

        public async Task<UsuarioDto> ObterUsuarioRolesByEmail(string email)
        {
            return await _db.Users.AsNoTracking()
                    .Include(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
                    .Select(usuario => new UsuarioDto 
                    {
                        Id = usuario.Id,
                        UserName = usuario.UserName,
                        Email = usuario.Email,
                        PhoneNumber = usuario.PhoneNumber,
                        Endereco = usuario.Endereco,
                        Roles = usuario.UserRoles.Select(ur => ur.Role.Name).ToList()
                    }).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UsuarioDto> ObterUsuarioRolesById(Guid id)
        {
            return await _db.Users.AsNoTracking()
                    .Include(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
                    .Select(usuario => new UsuarioDto 
                    {
                        Id = usuario.Id,
                        UserName = usuario.UserName,
                        Email = usuario.Email,
                        PhoneNumber = usuario.PhoneNumber,
                        Endereco = usuario.Endereco,
                        Role = usuario.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault()
                        
                    }).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<UsuarioDto>> ObterUsuariosRoles()
        {
            return await _db.Users.AsNoTracking()
                    .Include(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
                    .OrderBy(u => u.UserName)
                    .Select(usuario => new UsuarioDto 
                    {
                        Id = usuario.Id,
                        UserName = usuario.UserName,
                        Email = usuario.Email,
                        PhoneNumber = usuario.PhoneNumber,
                        Endereco = usuario.Endereco,
                        Role = usuario.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault()
                        
                    }).ToListAsync();
        }
    }
}