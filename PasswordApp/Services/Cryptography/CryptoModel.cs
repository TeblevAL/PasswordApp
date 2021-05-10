using PasswordApp.Services.Cryptography.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Services.Cryptography
{
    public class CryptoModel
    {
        public int Id { get; private set; }

        public string DisplayName { get; private set; }

        public ICryptography Algorithm { get; private set; }

        public CryptoModel(int id, string displayName, ICryptography algorithm)
        {
            Id = id;
            DisplayName = displayName;
            Algorithm = algorithm;
        }
    }
}
