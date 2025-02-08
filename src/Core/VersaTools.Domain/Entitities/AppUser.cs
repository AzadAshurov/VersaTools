
using Microsoft.AspNetCore.Identity;

namespace VersaTools.Domain.Entitities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }


    }
}
