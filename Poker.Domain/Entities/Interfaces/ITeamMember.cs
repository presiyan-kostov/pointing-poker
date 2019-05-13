namespace Poker.Domain.Entities.Interfaces
{
    public interface ITeamMember
    {
        #region -- properties --

        IUser User { get; }

        IProject Project { get; }

        #endregion

        #region -- methods --

        void Estimate(string issue, int estimation);

        #endregion
    }
}
