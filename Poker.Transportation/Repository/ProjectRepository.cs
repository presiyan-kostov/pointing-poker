using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Poker.Transportation.Entities;
using Poker.Transportation.Repository.Base;
using Poker.Transportation.Repository.Interfaces;

namespace Poker.Transportation.Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(ISession session) : base(session)
        {
        }

        public IList<Project> GetAllNotDeleted()
        {
            return Session.Query<Project>()
                          .Where(x => x.DeletedAt == null)
                          .ToList();
        }

        public Project GetByCode(string code)
        {
            return Session.Query<Project>()
                          .FirstOrDefault(x => x.Code == code &&
                                               x.DeletedAt == null);
        }
    }
}