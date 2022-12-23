using CompanyApp.Dal.Repo.Interfaces;
using CompanyApp.Models.Entities;
using CompanyApp.UI.Commands;
using CompanyApp.UI.Services.ShareEntity;
using CompanyApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CompanyApp.UI.ViewModels
{
    public class OrdersPageViewModel : ViewModelBase
    {
        private IOrderRepo dataRepo;
        private ISharedEntity<int?> idBrocker;
        public bool isInFocus { get; set; } = false;

        #region List<Order> Orders
        private List<Order> _orders = new();
        public List<Order> Orders 
        {
            get => _orders; 
            private set
            {
                if (_orders == value)
                    return;
                _orders = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public OrdersPageViewModel(ISharedEntity<int?> sharedIdBrocker, IOrderRepo repo)
        {
            idBrocker = sharedIdBrocker;
            dataRepo = repo;

            Orders = GetAllEntitiesFromRepo();
        }

        private List<Order> GetAllEntitiesFromRepo()
        {
            List<Order> entitiesFromDb = new();
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
        private string _title = "Список заказов";
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
                Window win = new Pages.EditOrderWindow();
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
                Window win = new Pages.EditOrderWindow();
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
                Orders = GetAllEntitiesFromRepo();
            },
            () => true
            );
        #endregion

        #endregion
    }
}
