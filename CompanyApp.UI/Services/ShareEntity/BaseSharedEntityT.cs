using CompanyApp.Models.Base;
using CompanyApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.UI.Services.ShareEntity
{
    public class BaseSharedEntity<T> : INotifyPropertyChanged, ISharedEntity<T>
    {
        private static Stack<T?> internalEntity = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void SetEntityToEdit(T entity) => internalEntity.Push(entity);

        public virtual T? GetEntityToEdit()
        {
            internalEntity.TryPop(out T? entityToReturn);
            internalEntity.Clear();
            return entityToReturn;
        }
    }
}
