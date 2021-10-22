using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace VirtualLibrary.Domain.Models
{
    public class Role : IdentityRole<Guid>
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}