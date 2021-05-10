using PasswordApp.Services.MVVMCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordApp.ViewModels
{
    public class MainWindowViewModel : MVVMBase
    {
        #region Приватные поля

        private MVVMBase selectedViewModel;
        private Window window;

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

        public ICommand MinimizedCommand { get; private set; }
        public ICommand MaximizedCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        #endregion

        #region Методы

        private void Minimized()
        {
            window.WindowState = WindowState.Minimized;
        }

        private void Maximized()
        {
            if (window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Normal;
            else if (window.WindowState == WindowState.Normal)
                window.WindowState = WindowState.Maximized;
        }

        private void Close()
        {
            window.Close();
        }

        public void LoadNewKeyViewModel()
        {

            window.MinHeight = 350;
            window.MinWidth = 550;

            window.Height = 350;
            window.Width = 550;

            SelectedViewModel = new NewKeyViewModel();
        }

        public void LoadLogInViewModel()
        {

            window.MinHeight = 350;
            window.MinWidth = 550;

            window.Height = 350;
            window.Width = 550;

            SelectedViewModel = new LoginViewModel();
        }


        #endregion

        #region Конструктор

        public MainWindowViewModel(Window window)
        {

            this.window = window;

            MinimizedCommand = new RelayCommand(obj => Minimized());
            MaximizedCommand = new RelayCommand(obj => Maximized());
            CloseCommand = new RelayCommand(obj => Close());

            MediatorMainWindow.Subscribe("LoadNewKeyView", LoadNewKeyViewModel);
            MediatorMainWindow.Subscribe("LoadLogInView", LoadLogInViewModel);
        }

        #endregion
    }
}
