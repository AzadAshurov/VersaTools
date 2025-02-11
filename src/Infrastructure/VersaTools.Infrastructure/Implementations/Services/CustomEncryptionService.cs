using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Application.Abstractions.Services;
using WertDrof.Encryption;
namespace VersaTools.Infrastructure.Implementations.Services
{
    public class CustomEncryptionService : ISecondPartOfPasswordService
    {
        public string CreatePassword(string input, string key)
        {
           string output = HashEncryption.Hasher(input, key, false);
            return output;
        }
    }
}
