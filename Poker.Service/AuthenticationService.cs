using Poker.Domain.Entities.Interfaces;
using Poker.Domain.Factories.Interfaces;
using Poker.Service.Interfaces;
using Poker.Transportation.Repository.Base.Interfaces;

namespace Poker.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        #region -- private readonly fields --

        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserFactory _userFactory;

        #endregion

        #region -- constructor --

        public AuthenticationService(IUnitOfWork unitOfWork, IUserFactory userFactory)
        {
            _unitOfWork = unitOfWork;

            _userFactory = userFactory;
        }

        #endregion

        #region -- public methods --

        public int? Authenticate(string username, string password)
        {
            IUser user = _userFactory.Get(username);

            if (user != null && user.CheckPassword(password))
            {
                return user.Id;
            }

            return null;
        }

        #endregion
    }
}