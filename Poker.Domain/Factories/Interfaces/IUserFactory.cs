using Poker.Domain.Entities.Interfaces;

namespace Poker.Domain.Factories.Interfaces
{
    public interface IUserFactory
    {
        IUser Get(int id);

        IUser Get(string username);

        IUser Get(Transportation.Entities.User user);

        IUser New(string username, string password);
    }
}