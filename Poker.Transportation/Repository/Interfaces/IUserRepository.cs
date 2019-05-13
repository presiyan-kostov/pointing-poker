using Poker.Transportation.Entities;
using Poker.Transportation.Repository.Base.Interfaces;

namespace Poker.Transportation.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetByUsername(string username);
    }
}