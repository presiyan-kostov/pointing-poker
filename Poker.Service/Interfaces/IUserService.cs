using System.Collections.Generic;
using Poker.Model.User;

namespace Poker.Service.Interfaces
{
    public interface IUserService
    {
        UserModel Get(int id);

        IList<ValidationError> Register(SaveModel model);

        IList<ValidationError> Validate(SaveModel model, bool checkUsernameExists);

        ValidationError? ValidateUsername(SaveModel model, bool checkUsernameExists);

        ValidationError? ValidatePassword(SaveModel model);

        ValidationError? ValidateFirstname(SaveModel model);

        ValidationError? ValidateLastname(SaveModel model);

        ValidationError? ValidateEmail(SaveModel model);
    }
}