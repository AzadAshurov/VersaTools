using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersaTools.Application.DTOs.AiDTO
{
    public class GenerateProductDescriptionRequest
    {
        public string ProductName { get; set; }
        public string Features { get; set; }
    }
}
