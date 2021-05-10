using Microsoft.Win32;
using PasswordApp.Models;
using PasswordApp.Services.Cryptography;
using PasswordApp.Services.MVVMCore;
using PasswordApp.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordApp.ViewModels
{
    public class NavigationViewModel : MVVMBase
    {
        #region Приватные поля

        private MVVMBase selectedViewModel;

        #endregion

        #region Свойства

        public MVVMBase SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        #endregion

        #region Комманды

        public ICommand HomeCommand { get; private set; }
        public ICommand EcsportCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        #endregion

        #region Методы

        private void Home()
        {
            SelectedViewModel = new HomeViewModel();
        }

        private void Ecsport()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PA files(*.pa)|*.pa";

            if (saveFileDialog.ShowDialog() == true)
            {
                KeyDialog keyDialog = new KeyDialog("Введите ключ для шифрования файла");
                if (keyDialog.ShowDialog() == true)
                {
                    List<Account> ecsportList = new List<Account>();
                    foreach (var account in ModelsController.Accounts)
                    {
                        ecsportList.Add(new Account { SiteName = account.SiteName, Login = account.Login, Password = account.Password, Url = account.Url });
                    }
                    CryptoController.Current.Algorithm.EncryptFile(saveFileDialog.FileName, ecsportList, keyDialog.Key);
                }
            }
        }

        private void Import()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PA files(*.pa)|*.pa";

            if (openFileDialog.ShowDialog() == true)
            {
                KeyDialog keyDialog = new KeyDialog("Введите ключ для расшифровки файла");
                if (keyDialog.ShowDialog() == true)
                {
                    List<Account> importList = CryptoController.Current.Algorithm.DecryptFile<Account>(openFileDialog.FileName, keyDialog.Key);
                    Category category = ModelsController.Categories.Where(ob => ob.Name == "[Импортированные]").FirstOrDefault();
                    if (category != null)
                    {
                        foreach (var account in importList)
                        {
                            account.IdCategory = category.Id;
                            ModelsController.AddAccount(account);
                        }

                    }
                    else
                    {
                        ModelsController.AddCategory(new Category { Name = "[Импортированные]" });
                        Category newCategory = ModelsController.Categories.Where(ob => ob.Name == "[Импортированные]").FirstOrDefault();
                        foreach (var account in importList)
                        {
                            account.IdCategory = newCategory.Id;
                            ModelsController.AddAccount(account);
                        }
                    }
                }
            }
        }

        private void Settings()
        {
            SelectedViewModel = new SettingsViewModel();
        }

        private void Exit()
        {
            ModelsController.EncryptPasswords();
            CryptoController.DeleteTrueKey();
            MediatorMainWindow.Notify("LoadLogInView");
        }
        #endregion

        #region Конструктор

        public NavigationViewModel()
        {

            HomeCommand = new RelayCommand(obj => Home());
            EcsportCommand = new RelayCommand(obj => Ecsport(),obj => ModelsController.Accounts.Count > 0);
            ImportCommand = new RelayCommand(obj => Import());
            SettingsCommand = new RelayCommand(obj => Settings());
            ExitCommand = new RelayCommand(obj => Exit());

            Home();
        }

        #endregion
    }
}
