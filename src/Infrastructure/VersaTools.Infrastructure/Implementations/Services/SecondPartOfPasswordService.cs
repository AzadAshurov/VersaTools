using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Infrastructure.Utilities.Hasher;

namespace VersaTools.Persistence.Implementations.Services
{
    internal class SecondPartOfPasswordService : ISecondPartOfPasswordService
    {
        public string CreatePassword(string input, string key)
        {
            return Hasher.HashString(input, key , 16,true,true);
        }
    }
}
