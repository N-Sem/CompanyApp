using System.Windows;

namespace CompanyApp.UI.Commands
{
    public class CloseApplicationCommand : CommandBase
    {
        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter) =>
            Application.Current.Shutdown();
    }
}
