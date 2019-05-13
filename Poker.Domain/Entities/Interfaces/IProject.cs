namespace Poker.Domain.Entities.Interfaces
{
    public interface IProject
    {
        #region -- properties --

        int Id { get; }

        string Code { get; set; }

        string YouTrackUrl { get; set; }

        string YouTrackQuery { get; set; }

        #endregion

        #region -- methods --

        void Save();

        void Delete();

        #endregion
    }
}