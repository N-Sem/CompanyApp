using CompanyApp.UI.Commands;
using CompanyApp.UI.ViewModels.Base;
using System.Windows;
using System.Windows.Controls;

namespace CompanyApp.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Page employeesPage { get; }
        private Page departmentsPage { get; }
        private Page ordersPage { get; }
        private Page settingsPage { get; }

        #region CurrentPage
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                if (value == _currentPage)
                    return;
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            employeesPage = new Pages.EmployeesPage();
            departmentsPage = new Pages.DepartmentsPage();
            ordersPage = new Pages.OrdersPage();
            settingsPage = new Pages.SettingsPage();

            _currentPage = employeesPage;
        }

        #region OpenEmployeesPageCommand
        public RelayCommand OpenEmployeesPageCommand =>
        new RelayCommand(
            () =>
            {
                CurrentPage = employeesPage;
            },
            () => CurrentPage != employeesPage
            );
        #endregion

        #region OpenDepartmentsPageCommand
        public RelayCommand OpenDepartmentsPageCommand =>
        new RelayCommand(
            () =>
            {
                CurrentPage = departmentsPage;
            },
            () => CurrentPage != departmentsPage
            );
        #endregion

        #region OpenOrdersPageCommand
        public RelayCommand OpenOrdersPageCommand =>
        new RelayCommand(
            () =>
            {
                CurrentPage = ordersPage;
            },
            () => CurrentPage != ordersPage
            );
        #endregion

        #region OpenSettingsPageCommand
        public RelayCommand OpenSettingsPageCommand =>
        new RelayCommand(
            () =>
            {
                Window win = new Pages.SettingsWindow();
                win.Show();
            },
            () => true
            );
        #endregion

        #region OpenHelpPageCommand
        public RelayCommand OpenHelpPageCommand =>
        new RelayCommand(
            () =>
            {
                Window win = new Pages.HelpWindow();
                win.Show();
            },
            () => true
            );
        #endregion
    }
}
