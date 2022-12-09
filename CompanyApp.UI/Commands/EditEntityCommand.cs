using CompanyApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyApp.UI.Commands
{
    public class EditEntityCommand : CommandBase
    {
        public override bool CanExecute(object? parameter) => CheckParameter(parameter);

        public override void Execute(object? parameter) =>
            OpenWindow(parameter);

        private void OpenWindow(object? parameter)
        {
            Window win = new Pages.Window1(parameter);
            win.Topmost = true;
            win.Show();
        }

        private bool CheckParameter(object? parameter) =>
            (parameter is not null) && (parameter is Employee || parameter is Department || parameter is Order);
    }
}
