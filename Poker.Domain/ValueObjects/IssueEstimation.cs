using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.ValueObjects.Interfaces;

namespace Poker.Domain.ValueObjects
{
    public class IssueEstimation : IIssueEstimation
    {
        #region -- private readonly fields --

        private readonly Transportation.Entities.IssueEstimation _issueEstimation;

        #endregion

        #region -- constructor --

        public IssueEstimation(IIssue issue, ITeamMember teamMember, IList<Transportation.Entities.IssueEstimation> issueEstimations)
        {
            Issue = issue;
            TeamMember = teamMember;
            _issueEstimation = issueEstimations.First(x => x.IsFinal);

            History = new List<IIssueEstimationHistory>();
            foreach (Transportation.Entities.IssueEstimation issueEstimation in issueEstimations)
            {
                History.Add(new IssueEstimationHistory(issueEstimation.Estimation, issueEstimation.CreatedAt));
            }
        }

        #endregion

        #region -- public properties --

        public IIssue Issue { get; }

        public ITeamMember TeamMember { get; }

        public int Estimation => _issueEstimation.Estimation;

        public DateTime CreatedAt => _issueEstimation.CreatedAt;

        public IList<IIssueEstimationHistory> History { get; }

        #endregion
    }
}