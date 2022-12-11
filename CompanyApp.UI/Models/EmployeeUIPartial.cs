using CompanyApp.Models.Base;
using CompanyApp.UI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.UI.Models
{
    public partial class EmployeeUI : BaseModelUI, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                ClearErrors(columnName);
                var errorsFromAnnotations = GetErrorsFromAnnotations(columnName,
                    typeof(EmployeeUI).GetProperty(columnName)?.GetValue(this, null));
                if (errorsFromAnnotations != null)
                {
                    AddErrors(columnName, errorsFromAnnotations);
                }
                var valueLength = ((string?)typeof(EmployeeUI).GetProperty(columnName)?.GetValue(this) ?? string.Empty).Length;
                switch (columnName)
                {
                    case nameof(FirstName):
                    case nameof(MiddleName):
                    case nameof(LastName):
                        return (valueLength > 50 || valueLength < 2) ? $"Поле {(string?)typeof(EmployeeUI).GetProperty(columnName)?.ToString()} должно содержать от 2 до 50 символов" : string.Empty ;
                    case nameof(BirthDate):
                        return (BirthDate > DateTime.Now.AddYears(-120) || BirthDate < DateTime.Now.AddYears(-18)) ? "Дата рождения установлена неправильно. Сотрруднику должно быть от 18 до 120 лет" : string.Empty;
                }
                return string.Empty;
            }
        }

        public string Error { get; }
    }
}
