using PasswordApp.Services.MVVMCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Models
{
    [KnownType(typeof(MVVMBase))]
    [DataContract]
    public class Category : MVVMBase
    {
        #region Приватные поля

        private string name;
        private ObservableCollection<Account> accounts;

        #endregion

        #region Публичные свойства

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Необходимо название категории")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        [DataMember]
        public virtual ObservableCollection<Account> Accounts
        {
            get { return accounts; }
            set
            {
                accounts = value;
                OnPropertyChanged("Accounts");
            }
        }

        #endregion
    }
}
