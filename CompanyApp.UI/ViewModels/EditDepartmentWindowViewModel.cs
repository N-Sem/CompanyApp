using CompanyApp.Dal.Repo;
using CompanyApp.Dal.Repo.Base;
using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Models.Entities;
using CompanyApp.UI.Commands;
using CompanyApp.UI.Models;
using CompanyApp.UI.Services.ShareEntity;
using CompanyApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyApp.UI.ViewModels
{
    public class EditDepartmentWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IDepartmentRepo deptRepo;
        private Department? department { get; set; }
        private int? departmentId { get; }

        public DepartmentUI DepartmentUI { get; set; } = new();
        public ObservableCollection<Employee> EmployeesInDepartment { get; } = new();
        public string WindowTitle { get; }

        public EditDepartmentWindowViewModel(ISharedEntity<int?> sharedEmployeeBrocker, IDepartmentRepo deptRepo)
        {
            this.deptRepo = deptRepo;

            departmentId = sharedEmployeeBrocker.GetEntityToEdit();

            if (departmentId is not null)
            {
                department = deptRepo.Find(departmentId);
            }

            WindowTitle = "Создать новый отдел";            

            if (department != null)
            {
                WindowTitle = "Редактировать отдел";

                DepartmentUI.Id = department.Id;
                DepartmentUI.Name = department.Name;

                var departmentDirector = deptRepo.GetDepartmentDirector(department.Id);
                DepartmentUI.DirectorName =(departmentDirector is not null) ? departmentDirector.FullName : string.Empty;

                var employeesInDepartment = deptRepo.GetAllEmployeesInDepartment(department.Id)!.OrderBy(o=>o.FirstName).ToList();

                foreach (var entity in employeesInDepartment)
                    DepartmentUI.Employees.Add(entity);

                DepartmentUI.IsChanged = false;
            }
            else
            {
                department = new();
            }
        }

        #region SaveEntityCommand
        public RelayCommand<Employee> SaveEntityCommand =>
        new RelayCommand<Employee>(
            (parameter) =>
            {
                if (!DepartmentUI.HasErrors)
                {
                    department!.Name = DepartmentUI.Name;
                    try
                    {
                        deptRepo.Update(department);
                        DepartmentUI.IsChanged = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            },
            (parameter) => DepartmentUI.IsChanged && !DepartmentUI.HasErrors
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
