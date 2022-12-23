using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Models.Entities;
using CompanyApp.UI.Commands;
using CompanyApp.UI.Services.ShareEntity;
using CompanyApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CompanyApp.UI.ViewModels;

public class EmployeesPageViewModel : ViewModelBase
{
    private IEmployeeRepo dataRepo;
    private ISharedEntity<int?> idBrocker;

    #region List<Employee> Employees
    private List<Employee> _employees = new();
    public List<Employee> Employees 
    { 
        get => _employees;
        private set
        {
            if (_employees == value)
                return;
            _employees = value;
            OnPropertyChanged();
        }
    }
    #endregion

    public EmployeesPageViewModel(ISharedEntity<int?> sharedIdBrocker, IEmployeeRepo repo)
    {
        idBrocker = sharedIdBrocker;
        dataRepo = repo;

        Employees = GetAllEntitiesFromRepo();
    }

    private List<Employee> GetAllEntitiesFromRepo()
    {
        List<Employee> entitiesFromDb = new();
        try
        {
            entitiesFromDb = dataRepo.GetAllAsNoTracking().ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return entitiesFromDb; 
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

    #region EditEntityCommand
    public RelayCommand EditEntityCommand =>
    new RelayCommand<int?>(
        (parameter) =>
        {
            idBrocker.SetEntityToEdit((int?)parameter);
            Window win = new Pages.EditEmployeeWindow();
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
            Window win = new Pages.EditEmployeeWindow();
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
            Employees = GetAllEntitiesFromRepo();
        },
        () => true
        );
    #endregion

    #endregion

    #region bool _isChanged
    private bool _isChanged = new();
    public bool IsChanged
    {
        get => _isChanged;
        set
        {
            if (_isChanged == value)
                return;
            _isChanged = value;
            OnPropertyChanged();
        }
    }
    #endregion
}
