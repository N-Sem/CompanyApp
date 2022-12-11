using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Enums;
using CompanyApp.Models.Entities;
using CompanyApp.UI.Commands;
using CompanyApp.UI.Models;
using CompanyApp.UI.Services;
using CompanyApp.UI.Services.ShareEntity;
using CompanyApp.UI.ViewModels.Base;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyApp.UI.ViewModels
{
    public class EditEmployeeWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public EmployeeUI EmployeeUI { get; set; } = new();
        public string WindowTitle { get; }
        private IEmployeeRepo repo { get; }
        private Employee employee { get; set; } = new ();
        int? employeeId { get;}

        public EditEmployeeWindowViewModel(ISharedEntity<int?> sharedEmployeeBrocker, IEmployeeRepo repo)
        {
            this.repo = repo;
            employeeId = sharedEmployeeBrocker.GetEntityToEdit();

            if (employeeId is not null)
            {
                employee = repo.Find(employeeId) ?? new Employee();
            }
            else
            {
                employee = new Employee();
            }
            
            WindowTitle = "Создать нового сотрудника";

            if (employee != null)
            {
                EmployeeUI.Id = employee.Id;
                EmployeeUI.FirstName = employee.FirstName;
                EmployeeUI.MiddleName = employee.MiddleName;
                EmployeeUI.LastName = employee.LastName;
                EmployeeUI.BirthDate = employee.BirthDate;
                EmployeeUI.Gender = employee.Gender;
                EmployeeUI.DepartmentId = employee.DepartmentId;
                EmployeeUI.IsDirector = employee.IsDirector;
                EmployeeUI.Orders = repo.GetAllEmployeeOrders(employee!.Id).ToList();

                WindowTitle = "Редактировать сотрудника";
            }
        }

        #region EditEmployeeCommand
        public RelayCommand<Employee> SaveEmployeeCommand =>
        new RelayCommand<Employee>(
            (parameter) =>
            {
                employee.FirstName = EmployeeUI.FirstName;
                employee.MiddleName = EmployeeUI.MiddleName;
                employee.LastName = EmployeeUI.LastName;
                employee.BirthDate = EmployeeUI.BirthDate;
                employee.Gender = EmployeeUI.Gender;
                employee.DepartmentId = EmployeeUI.DepartmentId;
                employee.IsDirector = EmployeeUI.IsDirector;
                employee.Orders = EmployeeUI.Orders;
                repo.Update(employee);
            },
            (parameter) => true
            );
        #endregion
    }
}
