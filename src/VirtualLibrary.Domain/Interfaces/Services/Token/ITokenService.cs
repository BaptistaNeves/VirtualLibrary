using System.Threading.Tasks;
using VirtualLibrary.Domain.DTOs.Login;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Services.Token
{
    public interface ITokenService
    {
        Task<string> CreateToken(string email);
    }
}