using Poker.Domain.Entities;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories.Interfaces;
using Poker.Transportation.Entities;

namespace Poker.Domain.Factories
{
    public class IssueFactory : IIssueFactory
    {
        #region -- private readonly fields --

        private readonly Transportation.Repository.Interfaces.IIssueEstimationRepository _issueEstimationRepository;

        private readonly IProjectFactory _projectFactory;

        #endregion

        #region -- constructor --

        public IssueFactory(Transportation.Repository.Interfaces.IIssueEstimationRepository issueEstimationRepository,
                            IProjectFactory projectFactory)
        {
            _issueEstimationRepository = issueEstimationRepository;
            _projectFactory = projectFactory;
        }

        #endregion

        #region -- public methods --

        public IIssue Get(IssueEstimation issueEstimation)
        {
            return new Issue(issueEstimation, _issueEstimationRepository, _projectFactory);
        }

        #endregion
    }
}