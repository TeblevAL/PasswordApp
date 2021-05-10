using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using PasswordApp.Services.DataAnnotations;
using PasswordApp.Services.MVVMCore;

namespace PasswordApp.Views.Dialogs
{
    public partial class KeyDialog : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Приватные поля

        private string key;
        private string titleHeader;

        #endregion

        #region Свойства

        [Required(ErrorMessage = "Не должно быть пустым")]
        [LatinString(ErrorMessage = "Допустимы только буквы латиского алфавита,цифры и особые знаки")]
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

        public string TitleHeader
        {
            get { return titleHeader; }
            set
            {
                titleHeader = value;
                OnPropertyChanged("TitleHeader");
            }
        }
        #endregion

        #region Комманды

        public ICommand EnterKeyCommand { get; private set; }

        #endregion

        #region Методы

        private void EnterKey()
        {
            this.DialogResult = true;
        }

        #endregion

        #region Конструктор

        public KeyDialog(string titleHeader)
        {
            InitializeComponent();
            DataContext = this;
            TitleHeader = titleHeader;
            EnterKeyCommand = new RelayCommand(obj => EnterKey(), obj => IsValid());

        }

        #endregion

        #region Реализация интерфейса INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        #region Реализация интерфейса IDataErrorInfo

        private Dictionary<string, ICollection<ValidationResult>> errors = new Dictionary<string, ICollection<ValidationResult>>();
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                PropertyInfo propertyInfo = this.GetType().GetProperty(columnName);
                errors[columnName] = new List<ValidationResult>();

                var result = Validator.TryValidateProperty(
                                          propertyInfo.GetValue(this, null),
                                          new ValidationContext(this, null, null)
                                          {
                                              MemberName = columnName
                                          },
                                          errors[columnName]);

                if (!result)
                {
                    var validationResult = errors[columnName].First();
                    return validationResult.ErrorMessage;
                }
                else
                {
                    errors[columnName].Clear();
                }

                return string.Empty;
            }
        }

        string IDataErrorInfo.Error => string.Empty;

        public bool IsValid()
        {
            Dictionary<string, ICollection<ValidationResult>>.ValueCollection messege = errors.Values;
            int count = 0;
            foreach (var i in messege)
            {
                count += i.Count;
            }
            if (count > 0)
                return false;
            else
                return true;

        }

        #endregion
    }
}
