using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Services.MVVMCore
{
    public class MVVMBase : INotifyPropertyChanged, IDataErrorInfo
    {
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

        #region Реализация интерфейса INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
