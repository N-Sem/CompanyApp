namespace CompanyApp.UI.Services.ShareEntity
{
    public interface ISharedEntity<T>
    {
        void SetEntityToEdit(T entity);
        T? GetEntityToEdit();
    }
}
