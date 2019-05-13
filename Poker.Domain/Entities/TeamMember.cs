using System;
using System.Linq;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories.Interfaces;
using Poker.Transportation.Entities;
using Poker.Transportation.Repository.Interfaces;

namespace Poker.Domain.Entities
{
    public class TeamMember : ITeamMember
    {
        #region -- private readonly fields --

        private readonly ProjectUser _projectUser;
        private readonly IIssueEstimationRepository _issueEstimationRepository;

        #endregion

        #region -- constructor --

        public TeamMember(ProjectUser projectUser,
                          IIssueEstimationRepository issueEstimationRepository,
                          IProjectFactory projectFactory,
                          IUserFactory userFactory)
        {
            _projectUser = projectUser;

            _issueEstimationRepository = issueEstimationRepository;

            User = userFactory.Get(projectUser.User);
            Project = projectFactory.Get(projectUser.Project);
        }

        #endregion

        #region -- public properties --

        public IUser User { get; }
   
        public IProject Project { get; }

        #endregion

        #region -- public methods --

        public void Estimate(string issue, int estimation)
        {
            IssueEstimation predecessor = _issueEstimationRepository.GetAll()
                                                                    .First(x => x.IsFinal &&
                                                                                x.ProjectUser.Id == _projectUser.Id &&
                                                                                x.Issue == issue);
            predecessor.IsFinal = false;
            _issueEstimationRepository.AddOrUpdate(predecessor);

            IssueEstimation issueEstimation = new IssueEstimation
                                              {
                                                  CreatedAt = DateTime.Now,
                                                  Estimation = estimation,
                                                  IsFinal = true,
                                                  Issue = issue,
                                                  ProjectUser = _projectUser
                                              };

            _issueEstimationRepository.AddOrUpdate(issueEstimation);
        }

        #endregion
    }
}