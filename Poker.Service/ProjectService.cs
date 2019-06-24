using System;
using System.Collections.Generic;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories.Interfaces;
using Poker.Model.Project;
using Poker.Service.Interfaces;
using Poker.Transportation.Repository.Base.Interfaces;

namespace Poker.Service
{
    public class ProjectService : IProjectService
    {
        #region -- private readonly fields --

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectFactory _projectFactory;
        private readonly IUserFactory _userFactory;

        #endregion

        #region -- constructor --

        public ProjectService(IUnitOfWork unitOfWork,
                              IProjectFactory projectFactory,
                              IUserFactory userFactory)
        {
            _unitOfWork = unitOfWork;
            _projectFactory = projectFactory;
            _userFactory = userFactory;
        }

        #endregion

        #region -- public methods --

        public ProjectModel Get(int projectId, int userId)
        {
            IProject project = _projectFactory.Get(projectId);

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

        public IList<ValidationError> Save(ProjectModel model)
        {
            IList<ValidationError> result;

            using (IGenericTransaction transaction = _unitOfWork.CreateTransaction())
            {
                result = Validate(model);

                if (result.Count > 0)
                {
                    transaction.Commit();

                    return result;
                }

                IProject project = model.Id.HasValue ? _projectFactory.Get(model.Id.Value) : _projectFactory.New();

                project.Code = model.Code.Trim();
                project.YouTrackUrl = model.YouTrackUrl.Trim();
                project.YouTrackQuery = model.YouTrackQuery.Trim();

                try
                {
                    project.Save();
                    model.Id = project.Id;

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return result;
        }

        public IList<ValidationError> Validate(ProjectModel model)
        {
            List<ValidationError> result = new List<ValidationError>();
            ValidationError? codeResult = ValidateCode(model);
            if (codeResult.HasValue)
            {
                result.Add(codeResult.Value);
            }

            ValidationError? youTrackUrlResult = ValidateYouTrackUrl(model);
            if (youTrackUrlResult.HasValue)
            {
                result.Add(youTrackUrlResult.Value);
            }

            ValidationError? youTrackQueryResult = ValidateYouTrackQuery(model);
            if (youTrackQueryResult.HasValue)
            {
                result.Add(youTrackQueryResult.Value);
            }

            return result;
        }

        public ValidationError? ValidateCode(ProjectModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Code))
            {
                return ValidationError.CodeMissing;
            }

            string code = model.Code.Trim();
            if (code.Length > 100)
            {
                return ValidationError.CodeToLong;
            }

            IProject project = _projectFactory.Get(model.Code);

            if (project != null && project.Id != model.Id)
            {
                return ValidationError.CodeExists;
            }

            return null;
        }

        public ValidationError? ValidateYouTrackUrl(ProjectModel model)
        {
            if (string.IsNullOrWhiteSpace(model.YouTrackUrl))
            {
                return ValidationError.YouTrackUrlMissing;
            }

            string youTrackUrl = model.YouTrackUrl.Trim();
            if (youTrackUrl.Length > 100)
            {
                return ValidationError.YouTrackUrlToLong;
            }

            return null;
        }

        public ValidationError? ValidateYouTrackQuery(ProjectModel model)
        {
            if (string.IsNullOrWhiteSpace(model.YouTrackQuery))
            {
                return ValidationError.YouTrackQueryMissing;
            }

            string youTrackQuery = model.YouTrackQuery.Trim();
            if (youTrackQuery.Length > 100)
            {
                return ValidationError.YouTrackQueryToLong;
            }

            return null;
        }

        #endregion
    }
}