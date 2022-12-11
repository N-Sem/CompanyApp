using CompanyApp.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.UI.Services.ShareEntity
{
    public interface ISharedEntity<T>
    {
        void SetEntityToEdit(T entity);
        T? GetEntityToEdit();
    }
}
