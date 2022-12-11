using CompanyApp.Enums;
using CompanyApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.UI.Models
{
    public partial class EmployeeUI : INotifyPropertyChanged
    {

        #region Id
        private int? _id;
        /// <summary>
        /// Id сотрудника
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

        #region FirstName
        private string? _firstName;
        /// <summary>
        /// Имя сотрудника
        /// </summary>        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? FirstName
        {
            get { return _firstName; }
            set
            {
                if (Equals(_firstName, value))
                    return;
                _firstName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region MiddleName
        private string? _middleName;
        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? MiddleName
        {
            get { return _middleName; }
            set
            {
                if (Equals(_middleName, value))
                    return;
                _middleName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region LastName
        private string? _lastName;
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? LastName
        {
            get { return _lastName; }
            set
            {
                if (Equals(_lastName, value))
                    return;
                _lastName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BirthDate
        private DateTime? _birthDate;
        /// <summary>
        /// Дата рождения сотрудника
        /// </summary>
        [Required]
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (Equals(_birthDate, value))
                    return;
                _birthDate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Gender
        private Gender? _gender;
        /// <summary>
        /// Пол сотрудника
        /// </summary>
        [Required]
        public Gender? Gender
        {
            get { return _gender; }
            set
            {
                if (Equals(_gender, value))
                    return;
                _gender = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DepartmentId
        private int? _departmentId;
        /// <summary>
        /// Id отдела, в корором работает сотрудник
        /// </summary>
        [Required]
        public int? DepartmentId
        {
            get { return _departmentId; }
            set
            {
                if (Equals(_departmentId, value))
                    return;
                _departmentId = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsDirector
        private bool _isDirector = false;
        /// <summary>
        /// Является ли сотрудник директором отдела
        /// </summary>
        public bool IsDirector
        {
            get { return _isDirector; }
            set
            {
                if (Equals(_isDirector, value))
                    return;
                _isDirector = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Orders
        private List<Order> _orders = new List<Order>();
        /// <summary>
        /// Список заказов сотрудника
        /// </summary>
        public List<Order> Orders
        {
            get { return _orders; }
            set
            {
                if (Equals(_orders, value))
                    return;
                _orders = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsChanged
        private bool _isChanged;
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                if (value == _isChanged)
                    return;
                OnPropertyChanged();
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
