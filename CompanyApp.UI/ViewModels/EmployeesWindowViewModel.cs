using CompanyApp.Dal.Repo;
using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Models.Entities;
using CompanyApp.UI.Commands;
using CompanyApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public EmployeesWindowViewModel()
        {
            try
            {
                dataRepo = new EmployeeRepo(DbContextConfigurationAndCreationHelpers.GetContext());
                Employees = dataRepo.GetAll().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string _title;

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


        private ICommand? _editEntityCommand = null;
        public ICommand EditEntityCommand => _editEntityCommand ?? new EditEntityCommand();
    }
}
