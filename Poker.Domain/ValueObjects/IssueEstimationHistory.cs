using System;
using Poker.Domain.ValueObjects.Interfaces;

namespace Poker.Domain.ValueObjects
{
    public class IssueEstimationHistory : IIssueEstimationHistory
    {
        #region -- constructor -- 

        public IssueEstimationHistory(int estimation, DateTime createAt)
        {
            Estimation = estimation;
            CreatedAt = createAt;
        }

        #endregion

        public int Estimation { get; }

        public DateTime CreatedAt { get; }
    }
}