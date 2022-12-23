using CompanyApp.Models.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CompanyApp.UI.Models
{
    public partial class OrderUI : INotifyPropertyChanged
    {
        #region Id
        private int? _id;
        /// <summary>
        /// Id заказа
        /// </summary>
        public int? Id
        {
            get { return _id; }
            set
            {
                if (Equals(_id, value))
                    return;
                _id = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Name
        private string? _name;
        /// <summary>
        /// Название заказа
        /// </summary>        
        [Required(ErrorMessage = "Необходимо указать название отдела")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Название должно содержать от 2 до 30 символов")]
        public string? Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value))
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Employees
        private ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();
        /// <summary>
        /// Список сотрудников, сделавших заказ
        /// </summary>
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                if (Equals(_employees, value))
                    return;
                _employees = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsChanged
        private bool _isChanged = false;
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                if (value == _isChanged) return;
                _isChanged = value;
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
