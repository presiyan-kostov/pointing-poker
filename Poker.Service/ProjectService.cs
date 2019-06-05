using System.Collections.Generic;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories.Interfaces;
using Poker.Model.Project;
using Poker.Service.Interfaces;

namespace Poker.Service
{
    public class ProjectService : IProjectService
    {
        #region -- private readonly fields --

        private readonly IProjectFactory _projectFactory;
        private readonly IUserFactory _userFactory;

        #endregion

        #region -- constructor --

        public ProjectService(IProjectFactory projectFactory,
                              IUserFactory userFactory)
        {
            _projectFactory = projectFactory;
            _userFactory = userFactory;
        }

        #endregion

        #region -- public methods --

        public ProjectModel Get(int id)
        {
            IProject project = _projectFactory.Get(id);

            if (project == null)
            {
                return null;
            }

            ProjectModel result = new ProjectModel
                                  {
                                      Id = project.Id,
                                      Code = project.Code,
                                      YouTrackQuery = project.YouTrackQuery,
                                      YouTrackUrl = project.YouTrackUrl
                                  };

            return result;
        }

        public IList<ProjectModel> GetForUser(int userId)
        {
            IUser user = _userFactory.Get(userId);
            if (user == null)
            {
                return null;
            }

            IList<ProjectModel> result = new List<ProjectModel>();
            IList<IProject> projects = user.GetProjects();

            foreach (IProject project in projects)
            {
                ProjectModel model = new ProjectModel
                {
                    Id = project.Id,
                    Code = project.Code,
                    YouTrackQuery = project.YouTrackQuery,
                    YouTrackUrl = project.YouTrackUrl
                };
                result.Add(model);
            }

            return result;
        }

        #endregion
    }
}