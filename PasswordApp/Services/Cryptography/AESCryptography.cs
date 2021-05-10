using PasswordApp.Services.Cryptography.Inteface;
using PasswordApp.Services.Serializations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordApp.Services.Cryptography
{
    public class AESCryptography : ICryptography
    {
        public string EncryptString(string text, string key)
        {
            string encryptText = string.Empty;

            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Key = new UnicodeEncoding().GetBytes(key);
            rijndael.IV = new UnicodeEncoding().GetBytes(key);

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetBytes(text).Length);
                    }
                    encryptText = Convert.ToBase64String(memoryStream.ToArray());
                }

                rijndael.Clear();

                return encryptText;
            }
            catch (Exception ex)
            {
                rijndael.Clear();
                MessageBox.Show(ex.Message);

                return text;
            }
        }

        public string DecryptString(string encryptText, string key)
        {
            string decryptText = null;

            RijndaelManaged rijndael = new RijndaelManaged();

            rijndael.Key = new UnicodeEncoding().GetBytes(key);
            rijndael.IV = new UnicodeEncoding().GetBytes(key);

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(Convert.FromBase64String(encryptText), 0, Convert.FromBase64String(encryptText).Length);
                    }
                    decryptText = Encoding.UTF8.GetString(memoryStream.ToArray());
                }

                rijndael.Clear();

                return decryptText;
            }
            catch (Exception ex)
            {
                rijndael.Clear();
                MessageBox.Show(ex.Message);

                return encryptText;
            }
        }

        public void EncryptFile<T>(string fileName, List<T> objectList, string key)
        {
            RijndaelManaged rijndael = new RijndaelManaged();
            try
            {
                rijndael.Key = new UnicodeEncoding().GetBytes(key);
                rijndael.IV = new UnicodeEncoding().GetBytes(key);

                using (Stream stream = File.Open(fileName, FileMode.Create))
                {
                    using (CryptoStream crStream = new CryptoStream(stream, rijndael.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        Serializer.Serialize(crStream, objectList);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<T> DecryptFile<T>(string fileName, string key)
        {
            RijndaelManaged rijndael = new RijndaelManaged();

            try
            {
                rijndael.Key = new UnicodeEncoding().GetBytes(key);
                rijndael.IV = new UnicodeEncoding().GetBytes(key);

                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    using (CryptoStream crStream = new CryptoStream(stream, rijndael.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        return Serializer.Deserialize<T>(crStream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<T>();
            }
        }

    }
}
