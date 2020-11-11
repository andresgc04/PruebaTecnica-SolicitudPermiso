using System;
using Entities.Data;

namespace DataAccess.Generic
{
    public interface IUnitOfWork : IDisposable
    {
        PermisosDBContext Context { get; }

        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public PermisosDBContext Context { get; }

        public UnitOfWork(PermisosDBContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
