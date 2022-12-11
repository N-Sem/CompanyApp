using CompanyApp.Dal.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWpfApp.ViewModels
{
    public class MainWindowViewModel
    {
        private IEmployeeRepo _repo;

        public MainWindowViewModel(IEmployeeRepo repo)
        {
            _repo = repo;
            Title = repo.Find(1)?.FirstName ?? "Заголовок окна";
        }

        public string Title { get; private set; }
    }
}
