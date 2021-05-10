using PasswordApp.Models;
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
    public partial class CategoryDialog : Window, INotifyPropertyChanged
    {
        #region Приватные поля

        private Category category;
        private string titleHeader;
        private string whatDo;

        #endregion

        #region Свойства

        public Category Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
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

        public ICommand EnterCategoryCommand { get; private set; }

        #endregion

        #region Методы

        private void EnterCategory()
        {
            this.DialogResult = true;
        }

        #endregion

        #region Конструктор

        public CategoryDialog(Category category, string title, string whatDo)
        {
            InitializeComponent();
            this.DataContext = this;

            Category = category;
            TitleHeader = title;
            WhatDo = whatDo;

            EnterCategoryCommand = new RelayCommand(obj => EnterCategory(), obj => Category.IsValid());
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
