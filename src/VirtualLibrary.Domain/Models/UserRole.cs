using System;
using Microsoft.AspNetCore.Identity;

namespace VirtualLibrary.Domain.Models
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public Role Role { get; set; }
        public Usuario Usuario { get; set; }
    }
}