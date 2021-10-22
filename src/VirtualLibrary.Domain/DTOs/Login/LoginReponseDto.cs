using System.Text.Json.Serialization;

namespace VirtualLibrary.Domain.DTOs.Login
{
    public class LoginReponseDto
    {
        public string AccessToken { get; set; }
        public UserTokenDto UserToken { get; set; }
    }
}