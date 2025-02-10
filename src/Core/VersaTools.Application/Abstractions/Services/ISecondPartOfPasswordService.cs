using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersaTools.Application.Abstractions.Services
{
    public interface ISecondPartOfPasswordService
    {
        public string CreatePassword(string input, string key);
    }
}
