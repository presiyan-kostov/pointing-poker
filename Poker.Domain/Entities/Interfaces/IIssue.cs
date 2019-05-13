namespace Poker.Domain.Entities.Interfaces
{
    public interface IIssue
    {
        #region -- properties --

        string Id { get; }

        IProject Project { get; }

        int Estimation { get; }

        #endregion
    }
}
