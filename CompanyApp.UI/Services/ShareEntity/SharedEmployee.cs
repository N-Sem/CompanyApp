using CompanyApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.UI.Services.ShareEntity
{
    public class SharedEmployee : BaseSharedEntity<Employee>, ISharedEntity<Employee>
    {
    }
}
