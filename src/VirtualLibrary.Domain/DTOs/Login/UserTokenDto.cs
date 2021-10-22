using System;
using System.Collections.Generic;

namespace VirtualLibrary.Domain.DTOs.Login
{
    public class UserTokenDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}