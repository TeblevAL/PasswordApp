using PasswordApp.Services.MVVMCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace PasswordApp.Views.Dialogs
{
    public partial class DeleteDialog : Window, INotifyPropertyChanged
    {
        #region Приватные поля

        private string titleHeader;
        private string message;

        #endregion

        #region Свойства

        public string TitleHeader
        {
            get { return titleHeader; }
            set
            {
                titleHeader = value;
                OnPropertyChanged("TitleHeader");
            }
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }
        #endregion

        #region Комманды

        public ICommand YesCommand { get; private set; }

        #endregion

        #region Методы

        private void Yes()
        {
            this.DialogResult = true;
        }

        #endregion

        #region Конструктор

        public DeleteDialog(string title, string message)
        {
            InitializeComponent();
            DataContext = this;

            TitleHeader = title;
            Message = message;

            YesCommand = new RelayCommand(obj => Yes());
        }

        #endregion

        #region Реализация интерфейса INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion

    }
}
