using CompanyApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.UI.Commands
{
    public class EditEntityCommand : CommandBase
    {
        public override bool CanExecute(object? parameter) => (parameter is Employee || parameter is Department || parameter is Order);

        public override void Execute(object? parameter)
        {
            if (parameter is Employee employee)
                employee.FirstName = "Pink";

            else if (parameter is Department department)
                department.Name = "NewDepartmentName";

            else if (parameter is Order order)
                order.Name = "NewOrderName";
        }
    }
}
