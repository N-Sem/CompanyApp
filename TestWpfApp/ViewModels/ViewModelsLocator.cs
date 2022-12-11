using Microsoft.Extensions.DependencyInjection;

namespace TestWpfApp.ViewModels;

public class ViewModelsLocator
{
    public MainWindowViewModel MainWindowViewModel => App.AppHost!.Services.GetRequiredService<MainWindowViewModel>();
}
