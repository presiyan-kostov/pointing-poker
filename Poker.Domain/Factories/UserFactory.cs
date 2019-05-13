using Poker.Domain.Entities;
using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories.Interfaces;
using Poker.Transportation.Repository.Interfaces;

namespace Poker.Domain.Factories
{
    public class UserFactory : IUserFactory
    {
        #region -- private readonly fields --

        private readonly IProjectUserRepository _projectUserRepository;
        private readonly IUserRepository _userRepository;

        private readonly IProjectFactory _projectFactory;

        public UserFactory(IProjectUserRepository projectUserRepository, IUserRepository userRepository, IProjectFactory projectFactory)
        {
            _projectUserRepository = projectUserRepository;
            _userRepository = userRepository;

            _projectFactory = projectFactory;
        }

        #endregion

        #region -- public methods

        public IUser Get(int id)
        {
            Transportation.Entities.User user = _userRepository.GetById(id);

            if (user == null || user.DeletedAt.HasValue)
            {
                return null;
            }

            return new User(user, _projectUserRepository, _userRepository, _projectFactory);
        }

        public IUser Get(string username)
        {
            Transportation.Entities.User user = _userRepository.GetByUsername(username);

            if (user == null || user.DeletedAt.HasValue)
            {
                return null;
            }

            return new User(user, _projectUserRepository, _userRepository, _projectFactory);
        }

        public IUser Get(Transportation.Entities.User user)
        {
            return new User(user, _projectUserRepository, _userRepository, _projectFactory);
        }

        public IUser New(string username, string password)
        {
            Transportation.Entities.User user = new Transportation.Entities.User
                                                {
                                                    Username = username,
                                                    Password = password
                                                };

            return new User(user, _projectUserRepository, _userRepository, _projectFactory);
        }

        #endregion
    }
}