using CompanyApp.UI.Models.Base;
using System;
using System.ComponentModel;

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
                var valueLength = ((string?)typeof(EmployeeUI).GetProperty(columnName)?.GetValue(this)?.ToString() ?? string.Empty).Length;
                switch (columnName)
                {
                    case nameof(FirstName):
                    case nameof(MiddleName):
                    case nameof(LastName):
                        return (valueLength > 30 || valueLength < 2) ? $"Поле {(string?)typeof(EmployeeUI).GetProperty(columnName)?.ToString()} должно содержать от 2 до 30 символов" : string.Empty;

                    case nameof(BirthDate):
                        return (BirthDate < DateTime.Now.AddYears(-100) || BirthDate > DateTime.Now.AddYears(-18)) ? "Дата рождения установлена неправильно. Сотрудник должен быть в возрасте от 18 до 100 лет" : string.Empty;
                }
                return string.Empty;
            }
        }

        public string Error { get; } = String.Empty;
    }
}
