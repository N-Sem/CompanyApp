using CompanyApp.UI.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyApp.UI.ViewModels
{
    public class ViewModelsLocator
    {
        public MainWindow MainWindow => App.AppHost.Services.GetRequiredService<MainWindow>();
        public MainWindowViewModel MainWindowViewModel => App.AppHost.Services.GetRequiredService<MainWindowViewModel>();

        public EditEmployeeWindowViewModel EditEmployeeWindowViewModel => App.AppHost.Services.GetRequiredService<EditEmployeeWindowViewModel>();
        public EditDepartmentWindowViewModel EditDepartmentWindowViewModel => App.AppHost.Services.GetRequiredService<EditDepartmentWindowViewModel>();
        public EditOrderWindowViewModel EditOrderWindowViewModel => App.AppHost.Services.GetRequiredService<EditOrderWindowViewModel>();

        public EmployeesPageViewModel EmployeesPageViewModel => App.AppHost.Services.GetRequiredService<EmployeesPageViewModel>();
        public DepartmentsPageViewModel DepartmentsPageViewModel => App.AppHost.Services.GetRequiredService<DepartmentsPageViewModel>();
        public OrdersPageViewModel OrdersPageViewModel => App.AppHost.Services.GetRequiredService<OrdersPageViewModel>();
        public SettingsPageViewModel SettingsPageViewModel => App.AppHost.Services.GetRequiredService<SettingsPageViewModel>();
    }
}
