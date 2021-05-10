using PasswordApp.Services.MVVMCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordApp.ViewModels
{
    public class NewKeyViewModel : MVVMBase
    {
        #region Приватные поля

        private string newKey;
        private string confirmKey;

        #endregion

        #region Свойства

        [Required(ErrorMessage = "Не должно быть пустым")]
        [MinLength(8, ErrorMessage = "Ключ должен состоять из 8 символов")]
        [MaxLength(8, ErrorMessage = "Ключ должен состоять из 8 символов")]
        [DataType(DataType.Password)]
        public string NewKey
        {
            get { return newKey; }
            set
            {
                newKey = value;
                OnPropertyChanged("NewKey");
            }
        }

        [Required(ErrorMessage = "Не должно быть пустым")]
        [DataType(DataType.Password)]
        [Compare("NewKey", ErrorMessage = "Ключи должны совпадать")]
        public string ConfirmKey
        {
            get { return confirmKey; }
            set
            {
                confirmKey = value;
                OnPropertyChanged("ConfirmKey");
            }
        }

        #endregion

        #region Комманды

        public ICommand AddKeyCommand { get; private set; }

        #endregion 

        #region Методы

        private void AddKey()
        {
        }

        #endregion

        #region Конструктор

        public NewKeyViewModel()
        {
            AddKeyCommand = new RelayCommand(obj => AddKey(), obj => IsValid());
        }

        #endregion
    }
}
