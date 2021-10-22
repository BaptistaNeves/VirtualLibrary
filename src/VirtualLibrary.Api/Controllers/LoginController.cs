using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Domain.DTOs.Login;
using VirtualLibrary.Domain.DTOs.Usuario;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Interfaces.Services.Token;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : MainController
    {
        private readonly SignInManager<Usuario> _signinManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public LoginController(INotifier notifier,
                             SignInManager<Usuario> signinManager,
                             UserManager<Usuario> userManager,
                             ITokenService tokenService,
                             IUsuarioRepository usuarioRepository,
                             IMapper mapper) : base(notifier)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginUser)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            // var usuario = await _usuarioRepository.ObterUsuarioRolesByEmail(loginUser.Email.ToLower());
            var usuario = await ObterUsuario(loginUser.Email);

            if(usuario == null)
            {
                NotificarErro("Senha ou Email Incorrectos!");
                return CustomResponse();
            }

            var result = await _signinManager.CheckPasswordSignInAsync(usuario, loginUser.Password, false);

            if(result.Succeeded)
            {
                var token = await _tokenService.CreateToken(usuario.Email);
                return CustomResponse
                (
                    new
                    {
                        Id = usuario.Id,
                        Email = usuario.Email,
                        UserName = usuario.UserName,
                        AccessToken = token,
                        Role = usuario.UserRoles.Select(r => r.Role.Name).FirstOrDefault()
                    }
                );
            }

            if(result.IsLockedOut)
            {
                NotificarErro("Úsuario Temporariamente bloqueado!");
                CustomResponse(loginUser);
            }

            NotificarErro("Email ou Senha Incorrectos!");
            return CustomResponse(loginUser);
        }

        [NonAction]
        public async Task<Usuario> ObterUsuario(string email)
        {
            return await _userManager.Users.AsNoTracking()
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .SingleOrDefaultAsync(u => u.Email == email);
        }
    }



}