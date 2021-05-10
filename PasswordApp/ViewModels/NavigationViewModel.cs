using Microsoft.Win32;
using PasswordApp.Services.MVVMCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordApp.ViewModels
{
    public class NavigationViewModel : MVVMBase
    {
        #region Приватные поля

        private MVVMBase selectedViewModel;

        #endregion

        #region Свойства

        public MVVMBase SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        #endregion

        #region Комманды

        public ICommand HomeCommand { get; private set; }
        public ICommand EcsportCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        #endregion

        #region Методы

        private void Home()
        {
           
        }

        private void Ecsport()
        {
           
        }

        private void Import()
        {
           
        }

        private void Settings()
        {
        }

        private void Exit()
        {
            MediatorMainWindow.Notify("LoadLogInView");
        }
        #endregion

        #region Конструктор

        public NavigationViewModel()
        {

            HomeCommand = new RelayCommand(obj => Home());
            EcsportCommand = new RelayCommand(obj => Ecsport());
            ImportCommand = new RelayCommand(obj => Import());
            SettingsCommand = new RelayCommand(obj => Settings());
            ExitCommand = new RelayCommand(obj => Exit());

            Home();
        }

        #endregion
    }
}
