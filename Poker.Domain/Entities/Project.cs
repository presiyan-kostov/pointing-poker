using System;
using Poker.Domain.Entities.Catalogues;
using Poker.Domain.Entities.Interfaces;
using Poker.Transportation.Repository.Interfaces;

namespace Poker.Domain.Entities
{
    public class Project : IProject
    {
        #region -- private readonly fields --

        private readonly Transportation.Entities.Project _project;
        private readonly Transportation.Repository.Interfaces.IProjectRepository _projectRepository;

        #endregion

        #region -- constructor --

        public Project(Transportation.Entities.Project project,
                       Transportation.Repository.Interfaces.IProjectRepository projectRepository)
        {
            _project = project;
            _projectRepository = projectRepository;
        }

        #endregion

        #region -- public properties 

        public int Id => _project.Id;

        public string Code
        {
            get => _project.Code;
            set => _project.Code = value;
        }

        public string YouTrackUrl
        {
            get => _project.YouTrackUrl;
            set => _project.YouTrackUrl = value;
        }

        public string YouTrackQuery
        {
            get => _project.YouTrackQuery;
            set => _project.YouTrackQuery = value;
        }

        #endregion

        #region -- public methods --

        public void Save()
        {
            _projectRepository.AddOrUpdate(_project);
        }

        public void Delete()
        {
            _project.DeletedAt = DateTime.Now;
            _projectRepository.AddOrUpdate(_project);
        }

        public void AddUser(int userId, Role role)
        {
        }

        public void DeleteUser(int userId)
        {
        }

        #endregion
    }
}