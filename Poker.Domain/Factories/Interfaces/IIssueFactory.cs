using Poker.Domain.Entities.Interfaces;

namespace Poker.Domain.Factories.Interfaces
{
    public interface IIssueFactory
    {
        IIssue Get(Transportation.Entities.IssueEstimation issueEstimation);
    }
}