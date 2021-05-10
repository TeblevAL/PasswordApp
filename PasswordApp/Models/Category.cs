using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Models
{
    public class Category
    {
        #region Приватные поля

        private string name;
        private ObservableCollection<Account> accounts;

        #endregion

        #region Публичные свойства

        public int Id { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public virtual ObservableCollection<Account> Accounts
        {
            get { return accounts; }
            set
            {
                accounts = value;
            }
        }

        #endregion
    }
}
