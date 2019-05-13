using System;

namespace Poker.Domain.ValueObjects.Interfaces
{
    public interface IIssueEstimationHistory
    {
        #region -- properties --

        int Estimation { get; }

        DateTime CreatedAt { get; }

        #endregion
    }
}