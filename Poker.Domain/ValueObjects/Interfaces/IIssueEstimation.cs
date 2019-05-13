using System;
using System.Collections.Generic;
using Poker.Domain.Entities.Interfaces;

namespace Poker.Domain.ValueObjects.Interfaces
{
    public interface IIssueEstimation
    {
        #region -- properties --

        IIssue Issue { get; }

        ITeamMember TeamMember { get; }

        int Estimation { get; }

        DateTime CreatedAt { get; }

        IList<IIssueEstimationHistory> History { get; }

        #endregion
    }
}