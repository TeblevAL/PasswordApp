using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Models
{
    public class Account
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

        public string SiteName
        {
            get { return siteName; }
            set
            {
                siteName = value;
            }
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
            }
        }

        public string Password
        {
            get { return password; }
            set
            {

                password = value;

            }
        }

        public string Url
        {
            get { return url; }
            set
            {
                url = value;
            }
        }

        public int IdCategory
        {
            get { return idCategory; }
            set
            {
                idCategory = value;

            }
        }

        public virtual Category Category
        {
            get { return category; }
            set
            {
                category = value;
            }
        }

        #endregion
    }
}
