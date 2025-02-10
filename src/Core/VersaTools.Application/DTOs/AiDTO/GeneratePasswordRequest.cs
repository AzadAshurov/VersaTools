using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersaTools.Application.DTOs.AiDTO
{
    public class GeneratePasswordRequest
    {
        public int Length { get; set; }
        public bool IncludeSymbols { get; set; }
        public bool IncludeNumbers { get; set; }
    }
}
