using System.Linq;
using NHibernate;
using Poker.Transportation.Entities;
using Poker.Transportation.Repository.Base;
using Poker.Transportation.Repository.Interfaces;

namespace Poker.Transportation.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {
        }

        public User GetByUsername(string username)
        {
            return Session.Query<User>()
                          .FirstOrDefault(x => x.Username == username && 
                                               x.DeletedAt == null);
        }
    }
}