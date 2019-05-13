using Poker.Domain.Entities;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories.Interfaces;

namespace Poker.Domain.Factories
{
    public class ProjectFactory : IProjectFactory
    {
        #region -- private readonly fields --

        private readonly Transportation.Repository.Interfaces.IProjectRepository _projectRepository;

        #endregion

        #region -- constructor --

        public ProjectFactory(Transportation.Repository.Interfaces.IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        #endregion

        #region -- public methods --

        public IProject Get(int id)
        {
            Transportation.Entities.Project project = _projectRepository.GetById(id);

            if (project == null || project.DeletedAt.HasValue)
            {
                return null;
            }

            return new Project(project, _projectRepository);
        }

        public IProject Get(Transportation.Entities.Project project)
        {
            return new Project(project, _projectRepository);
        }

        public IProject New()
        {
            Transportation.Entities.Project project = new Transportation.Entities.Project();

            return new Project(project, _projectRepository);
        }

        #endregion
    }
}