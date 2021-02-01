using CR.RoomBooking.Data.Contexts;
using CR.RoomBooking.Data.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CR.RoomBooking.Data.Repositories
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<long> SaveChangesAsync();
        DbSet<T> Table { get; }
        RoomBookingsContext Context { get; }
    }
}
