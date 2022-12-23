using CompanyApp.UI.Models.Base;
using System;
using System.ComponentModel;

namespace CompanyApp.UI.Models
{
    public partial class DepartmentUI : BaseModelUI, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                ClearErrors(columnName);
                var errorsFromAnnotations = GetErrorsFromAnnotations(columnName,
                    typeof(DepartmentUI).GetProperty(columnName)?.GetValue(this, null));
                if (errorsFromAnnotations != null)
                {
                    AddErrors(columnName, errorsFromAnnotations);
                }
                return string.Empty;
            }
        }

        public string Error { get; } = String.Empty;
    }
}
