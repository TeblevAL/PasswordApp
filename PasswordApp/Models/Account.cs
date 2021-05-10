using PasswordApp.Services.MVVMCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Models
{
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

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

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
