using System.Collections.Generic;
using System.ComponentModel;

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
