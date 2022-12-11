using CompanyApp.Dal.EfStructures;
using CompanyApp.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Dal.Repo.Base
{
    public abstract class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
    {
        private readonly bool _disposeContext;
        public ApplicationDbContext Context { get; }
        public DbSet<T> Table { get; }

        protected BaseRepo(ApplicationDbContext context)
        {
            Context = context;
            Table = context.Set<T>();
            _disposeContext = false;
        }

        protected BaseRepo(DbContextOptions<ApplicationDbContext> options)
            : this(new ApplicationDbContext(options))
        {
            _disposeContext = true;
        }
        
        #region Dispose/Finalize
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (_disposeContext)
                {
                    Context.Dispose();
                }
            }
            _isDisposed = true;
        }

        ~BaseRepo()
        {
            Dispose(false);
        }
        #endregion

        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            var entity = Table.Find(id);
            entity!.IsDeleted = true;
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Delete(T entity, bool persist = true)
        {
            entity.IsDeleted = true;
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            foreach (var entity in entities)
                entity.IsDeleted = true;
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public virtual T? Find(int? id) => Table.Find(id);

        public virtual T? FindAsNoTracking(int id) => Table.AsNoTrackingWithIdentityResolution().FirstOrDefault(x => x.Id == id);

        public virtual T? FindIgnoreQueryFilters(int id) => Table.IgnoreQueryFilters().FirstOrDefault(x => x.Id == id);

        public virtual IEnumerable<T> GetAll() => Table;

        public virtual IEnumerable<T> GetAllAsNoTracking() => Table.AsNoTrackingWithIdentityResolution();

        public virtual IEnumerable<T> GetAllIgnoreQueryFilters() => Table.IgnoreQueryFilters();

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while updating the database", ex);
            }
        }

        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }
    }
}
