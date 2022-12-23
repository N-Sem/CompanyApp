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
    public class EditOrderWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private IOrderRepo ordRepo;
        private Order? order { get; set; }
        private int? ordertId { get; }

        public OrderUI OrderUI { get; set; } = new();
        public ObservableCollection<Employee> EmployeesMadeOrder { get; } = new();
        public string WindowTitle { get; }

        public EditOrderWindowViewModel(ISharedEntity<int?> sharedEmployeeBrocker, IOrderRepo ordRepo)
        {
            this.ordRepo = ordRepo;

            ordertId = sharedEmployeeBrocker.GetEntityToEdit();

            if (ordertId is not null)
            {
                order = ordRepo.Find(ordertId);
            }

            WindowTitle = "Создать новый отдел";

            if (order != null)
            {
                WindowTitle = "Редактировать отдел";

                OrderUI.Id = order.Id;
                OrderUI.Name = order.Name;

                var employeesMadeOrder = ordRepo.GetAllEmployeesWhoHasOrder(order.Id)!.OrderBy(e=>e.FirstName).ToList();

                foreach (var entity in employeesMadeOrder)
                    OrderUI.Employees.Add(entity);

                OrderUI.IsChanged = false;
            }
            else
            {
                order = new();
            }
        }

        #region SaveEntityCommand
        public RelayCommand<Employee> SaveEntityCommand =>
        new RelayCommand<Employee>(
            (parameter) =>
            {
                if (!OrderUI.HasErrors)
                {
                    order!.Name = OrderUI.Name;
                    try
                    {
                        ordRepo.Update(order);
                        OrderUI.IsChanged = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            },
            (parameter) => OrderUI.IsChanged && !OrderUI.HasErrors
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
