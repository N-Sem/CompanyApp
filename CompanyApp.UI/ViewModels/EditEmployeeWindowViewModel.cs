using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Enums;
using CompanyApp.Models.Entities;
using CompanyApp.UI.Commands;
using CompanyApp.UI.Models;
using CompanyApp.UI.Services.ShareEntity;
using CompanyApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CompanyApp.UI.ViewModels
{
    public class EditEmployeeWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public EmployeeUI EmployeeUI { get; set; } = new();
        private bool employeeOrdersCollectionChanged = false;

        public List<Gender> GenderValues { get; } = new() { Gender.М, Gender.Ж };
        public List<Department> AllDepartments { get; }
        public List<Order> AllOrders { get; }

        public string WindowTitle { get; }

        private IEmployeeRepo empRepo { get; }
        private Employee? employee { get; set; } = new();
        private int? employeeId { get;}

        public EditEmployeeWindowViewModel(ISharedEntity<int?> sharedEmployeeBrocker, IEmployeeRepo empRepo, IDepartmentRepo deptRepo, IOrderRepo orderRepo)
        {
            AllDepartments = deptRepo.GetAll().OrderBy(o=>o.Name).ToList();
            AllOrders = orderRepo.GetAll().OrderBy(o=>o.Name).ToList();

            this.empRepo = empRepo;
            employeeId = sharedEmployeeBrocker.GetEntityToEdit();

            if (employeeId != null)
            {
                employee = empRepo.Find(employeeId);
            }
            
            WindowTitle = "Создать нового сотрудника";

            if (employee != null)
            {
                WindowTitle = "Редактировать сотрудника";

                EmployeeUI.Id = employee.Id;
                EmployeeUI.FirstName = employee.FirstName;
                EmployeeUI.MiddleName = employee.MiddleName;
                EmployeeUI.LastName = employee.LastName;
                EmployeeUI.BirthDate = employee.BirthDate;
                EmployeeUI.Gender = employee.Gender;
                EmployeeUI.DepartmentId = employee.DepartmentId;
                EmployeeUI.IsDirector = employee.IsDirector;
                var orders = empRepo.GetAllEmployeeOrders(employee!.Id).OrderBy(o=>o.Name).ToList();

                foreach (var order in orders)
                    EmployeeUI.Orders.Add(order);

                EmployeeUI.IsChanged = false;

                EmployeeUI.Orders.CollectionChanged += ((object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => { this.employeeOrdersCollectionChanged = true; }) ;
            }
            else
            {
                employee = new();
            }
        }

        #region SaveEmployeeCommand
        public RelayCommand<Employee> SaveEmployeeCommand =>
        new RelayCommand<Employee>(
            (parameter) =>
            {
                if (!EmployeeUI.HasErrors)
                {
                    employee.FirstName = EmployeeUI.FirstName;
                    employee.MiddleName = EmployeeUI.MiddleName;
                    employee.LastName = EmployeeUI.LastName;
                    employee.BirthDate = EmployeeUI.BirthDate;
                    employee.Gender = EmployeeUI.Gender;
                    employee.DepartmentId = EmployeeUI.DepartmentId;
                    employee.IsDirector = (EmployeeUI.DepartmentId != null) ? EmployeeUI.IsDirector : false;
                    employee.Orders = EmployeeUI.Orders.ToList();

                    try
                    {
                        if (EmployeeUI.Id == null && EmployeeUI.IsDirector && EmployeeUI.DepartmentId != null)
                            empRepo.AddNewEmployeeAsDepartmentDirector(employee, true);
                        else if (EmployeeUI.IsDirector && EmployeeUI.DepartmentId != null)
                        {
                            empRepo.Update(employee, false);
                            empRepo.SetDepartmentDirector((int)EmployeeUI.DepartmentId!,(int) EmployeeUI.Id!, false);
                            empRepo.SaveChanges();
                        }
                        else
                            empRepo.Update(employee, true);

                        employeeOrdersCollectionChanged = false;
                        EmployeeUI.IsChanged = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            },
            (parameter) => (EmployeeUI.IsChanged || employeeOrdersCollectionChanged) && !EmployeeUI.HasErrors
            );
        #endregion

        #region AddOrderCommand
        public RelayCommand<Order> AddOrderCommand =>
        new RelayCommand<Order>(
            (parameter) =>
            {
                if (EmployeeUI.Orders.Where(o => o.Id == parameter.Id).FirstOrDefault() is null)
                {
                    EmployeeUI.Orders.Add((Order)parameter);
                }
            },
            (parameter) => (parameter is not null) && EmployeeUI.Orders.Where(o => o.Id == parameter.Id).FirstOrDefault() == null
            );
        #endregion

        #region CloseWindowCommand
        public RelayCommand CloseWindowCommand =>
        new RelayCommand<Window>(
            (parameter) =>
            {
                parameter.Close();
            },
            (parameter) => true
            );
        #endregion
    }
}
