using CompanyApp.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompanyApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for SettinsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsPageViewModel ViewModel { get; set; }
        public SettingsWindow()
        {
            ViewModel = new();
            InitializeComponent();
        }
    }
}
