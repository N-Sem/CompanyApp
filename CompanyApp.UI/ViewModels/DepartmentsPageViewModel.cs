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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyApp.UI.ViewModels
{
    public class DepartmentsPageViewModel : ViewModelBase
    {
        private IDepartmentRepo dataRepo;
        private ISharedEntity<int?> idBrocker;

        #region List<DepartmentUI> Departments
        private List<DepartmentUI> _departments = new();
        public List<DepartmentUI> Departments 
        {
            get => _departments;
            private set
            {
                if (_departments == value)
                    return;
                _departments = value;
                OnPropertyChanged();
            }
        }
        #endregion


        public DepartmentsPageViewModel(ISharedEntity<int?> sharedIdBrocker, IDepartmentRepo repo)
        {
            idBrocker = sharedIdBrocker;
            dataRepo = repo;

            Departments = GetAllEntitiesFromRepo();
        }

        private List<DepartmentUI> GetAllEntitiesFromRepo()
        {
            List<Department> entitiesFromDb = new();
            List<DepartmentUI> departments = new();

            try
            {
                entitiesFromDb = dataRepo.GetAllAsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            foreach (var entity in entitiesFromDb)
            {
                var departmentDirector = dataRepo.GetDepartmentDirector(entity.Id);
                string directorName = (departmentDirector is not null) ? departmentDirector.FullName : string.Empty;
                departments.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    DirectorName = directorName
                });
            }
            return departments;
        }

        #region Title : string - Заголовок окна
        /// <summary>
        /// Заголовок окна
        /// </summary>
        private string _title = "Список отделов";
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

        #region EditEntityCommand
        public RelayCommand EditEntityCommand =>
        new RelayCommand<int?>(
            (parameter) =>
            {
                idBrocker.SetEntityToEdit((int?)parameter);
                Window win = new Pages.EditDepartmentWindow();
                win.Show();
            },
            (parameter) => parameter is not null
            );
        #endregion

        #region NewEntityCommand
        public RelayCommand NewEntityCommand =>
        new RelayCommand(
            () =>
            {
                Window win = new Pages.EditDepartmentWindow();
                win.Show();
            },
            () => true
            );
        #endregion

        #region UpdateListBox
        public RelayCommand UpdateListBox =>
        new RelayCommand(
            () =>
            {
                Departments = GetAllEntitiesFromRepo();
            },
            () => true
            );
        #endregion

        #endregion
    }
}
