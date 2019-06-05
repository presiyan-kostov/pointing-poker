using System.Collections.Generic;
using Poker.Model.Project;

namespace Poker.Service.Interfaces
{
    public interface IProjectService
    {
        ProjectModel Get(int id);

        IList<ProjectModel> GetForUser(int userId);
    }
}