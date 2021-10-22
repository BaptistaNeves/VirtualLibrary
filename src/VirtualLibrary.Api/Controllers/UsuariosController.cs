using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Domain.DTOs.Usuario;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Api.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    public class UsuariosController : MainController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IUsuarioRepository _userRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public UsuariosController(INotifier notifier,
                                 UserManager<Usuario> userManager,
                                 IMapper mapper,
                                 IUsuarioRepository userRepository, 
                                 RoleManager<Role> roleManager) : base(notifier)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ObterTodos()
        {
            return Ok(await _userRepository.ObterUsuariosRoles());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UsuarioDto>> ObterPorId(Guid id)
        {
            return await _userRepository.ObterUsuarioRolesById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(UsuarioDto usuario)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var usuarioMap = _mapper.Map<Usuario>(usuario);

            var result = await _userManager.CreateAsync(usuarioMap, usuario.Password);

            if(result.Succeeded )
            {
                var roleResult = await _userManager.AddToRoleAsync(usuarioMap, usuario.Role);

                if(!roleResult.Succeeded) AdicionarErro(roleResult.Errors);

                return CustomResponse(usuario);
            }

            AdicionarErro(result.Errors);

            return CustomResponse(usuario);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, UsuarioEditDto usuarioDto)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            if(usuario == null)
            {
                NotificarErro("Úsuario não Encotrado!");
                return CustomResponse();
            }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            await _userManager.UpdateSecurityStampAsync(usuario);

            usuario.UserName = usuarioDto.UserName;
            usuario.PhoneNumber = usuarioDto.PhoneNumber;
            usuario.Endereco = usuarioDto.Endereco;

            var result = await _userManager.UpdateAsync(usuario);

            if(result.Succeeded)
            {
                if(usuarioDto.Role.Any())
                {
                    var usuariosRoles = await _userRepository.ObterUsuarioRolesById(id);
                    await _userManager.RemoveFromRoleAsync(usuario, usuariosRoles.Role);
                    var roleResult = await _userManager.AddToRoleAsync(usuario, usuarioDto.Role);

                    if(!roleResult.Succeeded) AdicionarErro(roleResult.Errors);
                }
                
                return CustomResponse(usuarioDto);
            }

            AdicionarErro(result.Errors);

            return CustomResponse();
        }

        [HttpPut("atualizar-config/{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, UsuarioConfigDto usuarioConfig)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            if(usuario == null)
            {
                NotificarErro("Úsuario não Encotrado!");
                return CustomResponse();
            }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            // await _userManager.UpdateSecurityStampAsync(usuario);

            var code = await _userManager.GenerateChangeEmailTokenAsync(usuario, usuarioConfig.Email);

            var emailResult = await _userManager.ChangeEmailAsync(usuario, usuarioConfig.Email, code);
            if(emailResult.Succeeded)
            {
                var result = await _userManager.ChangePasswordAsync(usuario, usuarioConfig.CurrentPassword, usuarioConfig.Password);
                return CustomResponse(usuarioConfig);
            }
            
            AdicionarErro(emailResult.Errors);

            return CustomResponse(usuarioConfig);
        }

        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var usuario = await ObterUsuario(id);

            if(usuario == null)
            {
                NotificarErro("Úsuario Não encontrado!");
                return CustomResponse();
            }

            var result = await _userManager.DeleteAsync(usuario);

            if(result.Succeeded)
            {
                return CustomResponse();
            }

            AdicionarErro(result.Errors);

            return CustomResponse();
        }

        [HttpGet("obter-roles")]
        public async Task<ActionResult> ObterRoles() 
        {
            return Ok(await _roleManager.Roles.ToListAsync());
        }

        [NonAction]
        public void AdicionarErro(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                NotificarErro(error.Description);
            }
        }

        [NonAction]
        public async Task<Usuario> ObterUsuario(Guid id)
        {
            return await _userManager.Users
                        .AsNoTracking()
                        .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                        .SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}