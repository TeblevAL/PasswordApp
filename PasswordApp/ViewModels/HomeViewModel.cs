using PasswordApp.Models;
using PasswordApp.Services.MVVMCore;
using PasswordApp.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordApp.ViewModels
{
    public class HomeViewModel : MVVMBase
    {

        #region Приватные поля

        private ObservableCollection<Category> categories;
        private object selectedItem;
        private Visibility closePassword = Visibility.Visible;
        private Visibility openPassword = Visibility.Collapsed;

        #endregion

        #region Свойства

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                ClosePassword = Visibility.Visible;
                OpenPassword = Visibility.Collapsed;
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public Visibility ClosePassword
        {
            get { return closePassword; }
            set
            {
                closePassword = value;
                OnPropertyChanged("ClosePassword");
            }
        }

        public Visibility OpenPassword
        {
            get { return openPassword; }
            set
            {
                openPassword = value;
                OnPropertyChanged("OpenPassword");
            }
        }

        #endregion

        #region Комманды

        public ICommand AddAccountCommand { get; private set; }
        public ICommand EditAccountCommand { get; private set; }
        public ICommand DeleteAccountCommand { get; private set; }
        public ICommand AddCategoryCommand { get; private set; }
        public ICommand EditCategoryCommand { get; private set; }
        public ICommand DeleteCategoryCommand { get; private set; }
        public ICommand ShowPasswordCommand { get; private set; }
        public ICommand CopyPasswordCommand { get; private set; }
        public ICommand OpenSiteCommand { get; private set; }
        #endregion

        #region Методы

        private void AddAccount()
        {
            AccountDialog accountDialog = new AccountDialog(new Account(), Categories, "Добавление нового аккаунта", "Добавить");

            if (accountDialog.ShowDialog() == true)
            {
                ModelsController.AddAccount(accountDialog.Account);
            }
        }

        private void EditAccount()
        {
            Account editAccount = new Account
            {
                Id = (SelectedItem as Account).Id,
                SiteName = (SelectedItem as Account).SiteName,
                Login = (SelectedItem as Account).Login,
                Password = (SelectedItem as Account).Password,
                Url = (SelectedItem as Account).Url,
                Category = (SelectedItem as Account).Category
            };
            AccountDialog accountDialog = new AccountDialog(editAccount, Categories, "Изменение аккаунта", "Изменить");

            if (accountDialog.ShowDialog() == true)
            {
                ModelsController.EditAccount(accountDialog.Account);
            }
        }

        private void DeleteAccount()
        {
            Account account = SelectedItem as Account;
            DeleteDialog delete = new DeleteDialog("Удаление аккаунта", "Вы точно хотите удалить аккаунт?");

            if (delete.ShowDialog() == true)
            {
                ModelsController.DeleteAccount(account);
            }
        }

        private void AddCategory()
        {
            CategoryDialog categoryDialog = new CategoryDialog(new Category(), "Добавление нового каталога", "Добавить");

            if (categoryDialog.ShowDialog() == true)
            {
                ModelsController.AddCategory(categoryDialog.Category);
            }
        }

        private void EditCategory()
        {
            Category category = new Category
            {
                Id = (SelectedItem as Category).Id,
                Name = (SelectedItem as Category).Name
            };

            CategoryDialog categoryDialog = new CategoryDialog(category, "Изменение названия категории", "Изменить");

            if (categoryDialog.ShowDialog() == true)
            {
                ModelsController.EditCategory(categoryDialog.Category);
            }
        }

        private void DeleteCategory()
        {
            Category category = SelectedItem as Category;
            DeleteDialog deleteDialog = new DeleteDialog("Удаление категории", "Вы точно хотите удалить категорию?\n (Внимание:все аккаунты этой категории будут удаленны)");

            if (deleteDialog.ShowDialog() == true)
            {
                ModelsController.DeleteCategory(category);
            }
        }

        private void ShowPassword()
        {
            if (ClosePassword == Visibility.Visible)
            {
                ClosePassword = Visibility.Collapsed;
                OpenPassword = Visibility.Visible;
            }
            else
            {
                ClosePassword = Visibility.Visible;
                OpenPassword = Visibility.Collapsed;
            }

        }

        private void CopyPassword()
        {
            Clipboard.SetText((SelectedItem as Account).Password);
            MessageBox.Show("Пароль скопирован в буфер обмена");
        }

        private void OpenSite()
        {
            try
            {
                System.Diagnostics.Process.Start((SelectedItem as Account).Url);
            }
            catch
            {
                MessageBox.Show("Не удалось открыть");
            }

        }

        #endregion

        #region Конструктор

        public HomeViewModel()
        {
            Categories = ModelsController.Categories;

            AddAccountCommand = new RelayCommand(obj => AddAccount(), obj => Categories.Count > 0);
            EditAccountCommand = new RelayCommand(obj => EditAccount(), obj => SelectedItem is Account);
            DeleteAccountCommand = new RelayCommand(obj => DeleteAccount(), obj => SelectedItem is Account);
            AddCategoryCommand = new RelayCommand(obj => AddCategory());
            EditCategoryCommand = new RelayCommand(obj => EditCategory(), obj => SelectedItem is Category);
            DeleteCategoryCommand = new RelayCommand(obj => DeleteCategory(), obj => SelectedItem is Category);
            ShowPasswordCommand = new RelayCommand(obj => ShowPassword(), obj => SelectedItem is Account);
            CopyPasswordCommand = new RelayCommand(obj => CopyPassword(), obj => SelectedItem is Account);
            OpenSiteCommand = new RelayCommand(obj => OpenSite(), obj => SelectedItem is Account);
        }

        #endregion           

    }
}
