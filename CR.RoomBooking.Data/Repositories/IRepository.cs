using CR.RoomBooking.Data.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace CR.RoomBooking.Data.Repositories
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        long SaveChanges();
        DbSet<T> Table { get; }
    }
}
