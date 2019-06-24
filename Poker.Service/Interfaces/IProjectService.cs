using System.Collections.Generic;
using Poker.Model.Project;

namespace Poker.Service.Interfaces
{
    public interface IProjectService
    {
        ProjectModel Get(int projectId, int userId);

        IList<ProjectModel> GetForUser(int userId);

        IList<ValidationError> Save(ProjectModel model);
    }
}