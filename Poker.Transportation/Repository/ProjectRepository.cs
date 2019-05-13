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
    }
}