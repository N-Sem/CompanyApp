using CompanyApp.Dal.EfStructures;
using CompanyApp.Dal.Repo;
using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Models.Entities;
using CompanyApp.UI.Commands;
using CompanyApp.UI.Pages;
using CompanyApp.UI.Services.ShareEntity;
using CompanyApp.UI.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyApp.UI.ViewModels
{
    public class EmployeesWindowViewModel : ViewModelBase
    {
        private IEmployeeRepo? dataRepo;
        public IEnumerable<Employee> Employees { get; } = new List<Employee>();
        private ISharedEntity<int?> employeeBrocker;

        public EmployeesWindowViewModel(ISharedEntity<int?> sharedEmployeeBrocker, IEmployeeRepo repo)
        {
            employeeBrocker = sharedEmployeeBrocker;
            dataRepo = repo;

            try
            {
                Employees = dataRepo.GetAllAsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Title : string - Заголовок окна
        /// <summary>
        /// Заголовок окна
        /// </summary>
        private string _title = "Список сотрудников";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get { return _title; }
            set 
            {
                if (Equals(_title, value))
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands

        #region EditEmployeeCommand
        public RelayCommand EditEmployeeCommand =>
        new RelayCommand<int?>(
            (parameter) =>
            {
                employeeBrocker.SetEntityToEdit((int?)parameter);
                Window win = new Pages.EditEmployeeWindow();
                win.Show();
            },
            (parameter) => parameter is not null
            );
        #endregion

        #region NewEmployeeCommand
        public RelayCommand NewEmployeeCommand =>
        new RelayCommand(
            () =>
            {
                Window win = new Pages.EditEmployeeWindow();
                win.Show();
            },
            () => true
            );
        #endregion

        #endregion

    }
}
