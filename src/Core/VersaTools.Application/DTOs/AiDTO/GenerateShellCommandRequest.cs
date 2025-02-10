using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersaTools.Application.DTOs.AiDTO
{
    public class GenerateShellCommandRequest
    {
        public string Description { get; set; }
        public bool IsWindows { get; set; }
    }

}
