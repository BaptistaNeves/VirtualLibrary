using System;
using System.Security.Claims;
using System.Linq;

namespace VirtualLibrary.Api.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static Guid ObterIdUsuario(this ClaimsPrincipal usuario)
        {
            return Guid.Parse(usuario.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}