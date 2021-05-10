using PasswordApp.Services.Cryptography;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Models
{
    public static class ModelsController
    {
        public static ObservableCollection<Category> Categories { get; private set; }
        public static ObservableCollection<Account> Accounts { get; private set; }

        private static PasswordAppContext context;

        public static void StartUp()
        {
            context = new PasswordAppContext();

            context.Accounts.Load();
            context.Categories.Load();

            Categories = context.Categories.Local;
            Accounts = context.Accounts.Local;

        }

        public static void AddCategory(Category newCategory)
        {
            context.Categories.Local.Add(newCategory);
            context.SaveChanges();
        }
        public static void EditCategory(Category editCategory)
        {
            Category category = context.Categories.Find(editCategory.Id);
            category.Name = editCategory.Name;
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }
        public static void DeleteCategory(Category category)
        {
            context.Categories.Local.Remove(category);
            context.SaveChanges();
        }

        public static void AddAccount(Account newAccount)
        {
            context.Accounts.Local.Add(newAccount);
            context.SaveChanges();
        }
        public static void EditAccount(Account editAccount)
        {
            Account account = context.Accounts.Find(editAccount.Id);

            account.SiteName = editAccount.SiteName;
            account.Login = editAccount.Login;
            account.Password = editAccount.Password;
            account.Url = editAccount.Url;
            account.IdCategory = editAccount.IdCategory;

            context.Entry(account).State = EntityState.Modified;
            context.SaveChanges();
        }
        public static void DeleteAccount(Account account)
        {
            context.Accounts.Local.Remove(account);
            context.SaveChanges();
        }

        public static void EncryptPasswords()
        {
            foreach (var account in Accounts)
            {
                account.Password = CryptoController.Current.Algorithm.EncryptString(account.Password, CryptoController.TrueKey);
            }
            context.SaveChanges();
        }

        public static void DecryptPasswords()
        {
            foreach (var account in Accounts)
            {
                account.Password = CryptoController.Current.Algorithm.DecryptString(account.Password, CryptoController.TrueKey);
            }
        }
    }
}
