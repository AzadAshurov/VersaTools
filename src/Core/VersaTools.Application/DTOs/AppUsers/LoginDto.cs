﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersaTools.Application.DTOs.AppUsers
{
    public record LoginDto(string UserNameOrEmail, string Password);
}
