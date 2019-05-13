using NHibernate;
using Poker.Transportation.Entities;
using Poker.Transportation.Repository.Base;
using Poker.Transportation.Repository.Interfaces;

namespace Poker.Transportation.Repository
{
    public class IssueEstimationRepository : RepositoryBase<IssueEstimation>, IIssueEstimationRepository
    {
        public IssueEstimationRepository(ISession session) : base(session)
        {
        }
    }
}