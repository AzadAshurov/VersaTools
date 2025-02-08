using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersaTools.Application.DTOs.AppUsers
{
    public record RegisterDto(string Name, string Surname,string UserName,  string Email,string Password );
}
