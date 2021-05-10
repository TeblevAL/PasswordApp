using PasswordApp.Services.DataAnnotations;
using PasswordApp.Services.MVVMCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Models
{
    [KnownType(typeof(MVVMBase))]
    [DataContract]
    public class Account : MVVMBase
    {
        #region Приватные поля

        private string siteName;
        private string login;
        private string password;
        private string url;
        private int idCategory;
        private Category category;

        #endregion

        #region Публичные свойства

        public int Id { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Введите название сайта/платформы")]
        public string SiteName
        {
            get { return siteName; }
            set
            {
                siteName = value;
                OnPropertyChanged("SiteName");
            }
        }

        [DataMember]
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        [DataMember]
        [Required(ErrorMessage = "Необходим пароль")]
        public string Password
        {
            get { return password; }
            set
            {

                password = value;
                OnPropertyChanged("Password");
            }
        }

        [DataMember]
        [UrlString(ErrorMessage = "Введите коректный URL адрес")]
        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged("Url");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Выберете категорию")]
        public int IdCategory
        {
            get { return idCategory; }
            set
            {
                idCategory = value;
                OnPropertyChanged("IdCategory");
            }
        }

        public virtual Category Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }

        #endregion
    }
}
