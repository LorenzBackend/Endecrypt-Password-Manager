using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endecrypt.Core
{
    interface IEncryptor
    {
        string Encrypt(string input, string rule);
        string Decrypt(string input, string rule);
    }
}
