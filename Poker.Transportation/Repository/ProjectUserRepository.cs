using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Poker.Transportation.Entities;
using Poker.Transportation.Repository.Base;
using Poker.Transportation.Repository.Interfaces;

namespace Poker.Transportation.Repository
{
    public class ProjectUserRepository : RepositoryBase<ProjectUser>, IProjectUserRepository
    {
        #region -- constructor --

        public ProjectUserRepository(ISession session) : base(session)
        {
        }

        #endregion

        #region -- public methods --

        public IList<ProjectUser> GetByUser(int userId)
        {
            return Session.Query<ProjectUser>()
                          .Where(x => x.User.Id == userId)
                          .ToList();
        }

        #endregion
    }
}