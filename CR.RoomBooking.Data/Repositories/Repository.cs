using CR.RoomBooking.Data.Contexts;
using CR.RoomBooking.Data.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace CR.RoomBooking.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private bool isDisposed = false;
        protected readonly RoomBookingsContext _context;

        public Repository(RoomBookingsContext context)
        {
            _context = context;
        }

        public virtual DbSet<T> Table => _context.Set<T>();

        public virtual void Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public virtual long SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
