﻿using PasswordApp.Services.MVVMCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordApp.ViewModels
{
    public class LoginViewModel : MVVMBase
    {
        #region Приватные поля

        private string key;

        #endregion     

        #region Комманды

        public ICommand LogInCommand { get; private set; }

        #endregion

        #region Свойства

        [Required(ErrorMessage = "Не должно быть пустым")]
        [MinLength(8, ErrorMessage = "Ключ должен состоять из 8 символов")]
        [MaxLength(8, ErrorMessage = "Ключ должен состоять из 8 символов")]
        public string Key
        {
            get { return key; }

            set
            {
                key = value;
                OnPropertyChanged("Key");
            }
        }

        #endregion

        #region Методы

        private void LogIn()
        {

           
        }

        #endregion

        #region Конструктор

        public LoginViewModel()
        {
            LogInCommand = new RelayCommand(obj => LogIn(), obj => IsValid());
        }

        #endregion
    }
}
