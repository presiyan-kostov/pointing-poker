using System.Collections.Generic;

namespace Poker.Domain.Entities.Interfaces
{
    public interface IUser
    {
        #region -- propeties --

        int Id { get; }

        string Username { get; }

        string Firstname { get; set; }

        string Lastname { get; set; }

        string Email { get; set; }

        bool IsAdmin { get; set; }

        #endregion

        #region -- public methods --

        bool CheckPassword(string password);

        IList<IProject> GetProjects();

        void Save();

        void Delete();

        #endregion
    }
}