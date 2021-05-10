using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Services.Cryptography.Inteface
{
    public interface ICryptography
    {
        //Шифрование строки
        string EncryptString(string text, string key);

        //Расшифровка строки
        string DecryptString(string encryptText, string key);

        //Шифрование колекции обьектов
        void EncryptFile<T>(string fileName, List<T> objectList, string key);

        //Расшифровка колекции обьектов
        List<T> DecryptFile<T>(string fileName, string key);
    }
}
