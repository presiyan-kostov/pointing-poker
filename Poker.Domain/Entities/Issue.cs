using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories.Interfaces;
using Poker.Transportation.Entities;

namespace Poker.Domain.Entities
{
    public class Issue : IIssue
    {
        #region -- private readonly fields --

        private readonly IssueEstimation _issueEstimation;
        private readonly Transportation.Repository.Interfaces.IIssueEstimationRepository _issueEstimationRepository;

        private readonly IProjectFactory _projectFactory;

        #endregion

        #region -- private fields --

        private IProject _project;

        #endregion

        #region -- constructor --

        public Issue(IssueEstimation issueEstimation, 
                     Transportation.Repository.Interfaces.IIssueEstimationRepository issueEstimationRepository,
                     IProjectFactory projectFactory)
        {
            _issueEstimation = issueEstimation;
            _issueEstimationRepository = issueEstimationRepository;
            _projectFactory = projectFactory;
        }

        #endregion

        #region -- public properties --

        public string Id => _issueEstimation.Issue;

        public IProject Project => _project ?? (_project = _projectFactory.Get(_issueEstimation.ProjectUser
                                                                                               .Project));

        public int Estimation => CalculateEstimation();

        #endregion

        #region -- private methods --

        private int CalculateEstimation()
        {
            IList<IssueEstimation> issueEstimations = _issueEstimationRepository.GetAll()
                                                                                .Where(x => x.Issue == Id &&
                                                                                            x.ProjectUser
                                                                                             .Project
                                                                                             .Id == _issueEstimation.ProjectUser
                                                                                                                    .Project
                                                                                                                    .Id &&
                                                                                            x.IsFinal)
                                                                                .ToList();
            if (!issueEstimations.Any())
            {
                return 0;
            }

            int result = issueEstimations.Sum(x => x.Estimation) / issueEstimations.Count();

            return result;
        }

        #endregion
    }
}