using CR.RoomBooking.Data.Contexts;
using CR.RoomBooking.Data.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CR.RoomBooking.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private bool isDisposed = false;
        public RoomBookingsContext Context { get; private set; }

        public Repository(RoomBookingsContext context)
        {
            Context = context;
        }

        public virtual DbSet<T> Table => Context.Set<T>();

        public virtual void Add(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
        }

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }

        public async virtual Task<long> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
