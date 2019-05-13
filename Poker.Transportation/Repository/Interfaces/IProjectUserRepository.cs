using System.Collections.Generic;
using Poker.Transportation.Entities;
using Poker.Transportation.Repository.Base.Interfaces;

namespace Poker.Transportation.Repository.Interfaces
{
    public interface IProjectUserRepository : IRepositoryBase<ProjectUser>
    {
        IList<ProjectUser> GetByUser(int userId);
    }
}