using CompanyApp.UI.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.UI.ViewModels
{
    public class ViewModelsLocator
    {
        public EmployeesWindow EmployeesWindow => App.AppHost.Services.GetRequiredService<EmployeesWindow>();
        public EditEmployeeWindow EditEmployeeWindow => App.AppHost.Services.GetRequiredService<EditEmployeeWindow>();

        public EmployeesWindowViewModel EmployeesWindowViewModel => App.AppHost.Services.GetRequiredService<EmployeesWindowViewModel>();
        public EditEmployeeWindowViewModel EditEmployeeWindowViewModel => App.AppHost.Services.GetRequiredService<EditEmployeeWindowViewModel>();
    }
}
