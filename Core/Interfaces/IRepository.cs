using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);

        ICollection<T> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to);

    }
}