using PasswordApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Services.Cryptography
{
    public static class CryptoController
    {
        public static List<CryptoModel> Cryptographies { get; private set; }
        public static CryptoModel Current { get; private set; }
        public static string TrueKey { get; private set; }

        public static void StartUp()
        {
            Cryptographies = new List<CryptoModel>
            {
                new CryptoModel(1,"DES-алгоритм", new DESCryptography()),
                new CryptoModel(2,"AES-алгоритм",new AESCryptography())
            };

            Current = Cryptographies.Where(obj => obj.Id == Settings.Default.CryptoId).FirstOrDefault();
        }

        public static void ChangeCrypto(CryptoModel cryptoModel)
        {
            Current = cryptoModel;
            Settings.Default.CryptoId = cryptoModel.Id;
            Settings.Default.Save();
            AddKey(TrueKey);
        }

        public static void AddKey(string key)
        {
            Settings.Default.Key = Current.Algorithm.EncryptString(key, key);
            Settings.Default.Save();
            TrueKey = key;
        }

        public static bool DecryptKey(string key)
        {
            if (key == Current.Algorithm.DecryptString(Settings.Default.Key, key))
            {
                TrueKey = key;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckKey()
        {
            if (string.IsNullOrEmpty(Settings.Default.Key))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void DeleteTrueKey()
        {
            TrueKey = string.Empty;
        }
    }
}
