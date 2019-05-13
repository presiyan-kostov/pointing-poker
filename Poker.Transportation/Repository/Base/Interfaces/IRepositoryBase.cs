using System.Collections.Generic;

namespace Poker.Transportation.Repository.Base.Interfaces
{
    public interface IRepositoryBase<T>
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        void AddOrUpdate(T model);
    }
}