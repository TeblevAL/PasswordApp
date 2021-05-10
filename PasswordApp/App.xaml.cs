using PasswordApp.Models;
using PasswordApp.Services.Cryptography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordApp
{
    public partial class App : Application
    {
        public ResourceDictionary ThemeDictionary
        {
            get { return Resources.MergedDictionaries[0]; }
        }

        public ResourceDictionary ColorDictionary
        {
            get { return Resources.MergedDictionaries[2]; }
        }

        public void ChangeTheme(Uri uri)
        {
            ThemeDictionary.MergedDictionaries.Clear();
            ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }

        public void ChangeColor(Uri uri)
        {
            ColorDictionary.MergedDictionaries.Clear();
            ColorDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }


        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            if (!string.IsNullOrEmpty(CryptoController.TrueKey))
            {
                ModelsController.EncryptPasswords();
            }
        }
    }
}
