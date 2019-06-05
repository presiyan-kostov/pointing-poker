﻿using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories.Interfaces;

namespace Poker.Domain.Entities
{
    public class User : IUser
    {
        #region -- private readonly fields --

        private readonly Transportation.Entities.User _user;
        private readonly Transportation.Repository.Interfaces.IProjectUserRepository _projectUserRepository;
        private readonly Transportation.Repository.Interfaces.IProjectRepository _projectRepository;
        private readonly Transportation.Repository.Interfaces.IUserRepository _userRepository;

        private readonly IProjectFactory _projectFactory;

        #endregion

        #region -- constructor --

        public User(Transportation.Entities.User user,
                    Transportation.Repository.Interfaces.IProjectUserRepository projectUserRepository,
                    Transportation.Repository.Interfaces.IProjectRepository projectRepository,
                    Transportation.Repository.Interfaces.IUserRepository userRepository,
                    IProjectFactory projectFactory)
        {
            _user = user;
            _projectUserRepository = projectUserRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;

            _projectFactory = projectFactory;
        }

        #endregion

        #region -- public properties --

        public int Id => _user.Id;

        public string Username => _user.Username;

        public string Firstname
        {
            get => _user.Firstname;
            set => _user.Firstname = value;
        }

        public string Lastname
        {
            get => _user.Lastname;
            set => _user.Lastname = value;
        }

        public string Email
        {
            get => _user.Email;
            set => _user.Email = value;
        }

        public bool IsAdmin
        {
            get => _user.IsAdmin;
            set => _user.IsAdmin = value;
        }

        #endregion

        #region -- public methods --

        public bool CheckPassword(string password)
        {
            return _user.Password.Equals(password);
        }

        public IList<IProject> GetProjects()
        {
            IEnumerable<Transportation.Entities.Project> entities;

            if (IsAdmin)
            {
                entities = _projectRepository.GetAllNotDeleted();
            }
            else
            {
                entities = _projectUserRepository.GetByUser(_user.Id).Select(x => x.Project);
            }

            return entities.Select(x => _projectFactory.Get(x)).ToList();
        }

        public void Save()
        {
            _userRepository.AddOrUpdate(_user);
        }

        public void Delete()
        {
            _user.DeletedAt = DateTime.Now;
            _userRepository.AddOrUpdate(_user);
        }

        #endregion
    }
}