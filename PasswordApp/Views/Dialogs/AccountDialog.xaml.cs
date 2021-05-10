using PasswordApp.Models;
using PasswordApp.Services.MVVMCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace PasswordApp.Views.Dialogs
{
    public partial class AccountDialog : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Приватные поля

        private Account account;
        private ObservableCollection<Category> categories;
        private string titleHeader;
        private string whatDo;

        #endregion

        #region Свойства

        public Account Account
        {
            get { return account; }
            set
            {
                account = value;
                OnPropertyChanged("Account");
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public int IdCategory
        {
            get { return Account.IdCategory; }
            set
            {
                Account.IdCategory = value;
                OnPropertyChanged("IdCategory");
            }
        }
        public string TitleHeader
        {
            get { return titleHeader; }
            set
            {
                titleHeader = value;
                OnPropertyChanged("TitleHeader");
            }
        }
        public string WhatDo
        {
            get { return whatDo; }
            set
            {
                whatDo = value;
                OnPropertyChanged("WhatDo");
            }
        }

        #endregion

        #region Комманды

        public ICommand EnterAccountCommand { get; private set; }

        #endregion

        #region Методы

        private void EnterAccount()
        {
            this.DialogResult = true;
        }

        #endregion

        #region Конструктор

        public AccountDialog(Account newAccount, ObservableCollection<Category> categories, string title, string whatDo)
        {

            InitializeComponent();
            this.DataContext = this;

            Account = newAccount;
            Categories = categories;

            if (Account.IdCategory == 0)
            {
                IdCategory = Categories.FirstOrDefault().Id;
            }
            else
            {
                IdCategory = Account.IdCategory;
            }

            TitleHeader = title;
            WhatDo = whatDo;

            EnterAccountCommand = new RelayCommand(obj => EnterAccount(), obj => Account.IsValid() && IsValid());

        }

        #endregion

        #region Реализация интерфейса INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        #region Реализация интерфейса IDataErrorInfo

        private Dictionary<string, ICollection<ValidationResult>> errors = new Dictionary<string, ICollection<ValidationResult>>();
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                PropertyInfo propertyInfo = this.GetType().GetProperty(columnName);
                errors[columnName] = new List<ValidationResult>();

                var result = Validator.TryValidateProperty(
                                          propertyInfo.GetValue(this, null),
                                          new ValidationContext(this, null, null)
                                          {
                                              MemberName = columnName
                                          },
                                          errors[columnName]);

                if (!result)
                {
                    var validationResult = errors[columnName].First();
                    return validationResult.ErrorMessage;
                }
                else
                {
                    errors[columnName].Clear();
                }

                return string.Empty;
            }
        }

        string IDataErrorInfo.Error => string.Empty;

        public bool IsValid()
        {
            Dictionary<string, ICollection<ValidationResult>>.ValueCollection messege = errors.Values;
            int count = 0;
            foreach (var i in messege)
            {
                count += i.Count;
            }
            if (count > 0)
                return false;
            else
                return true;

        }

        #endregion
    }
}
