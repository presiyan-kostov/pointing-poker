namespace Poker.Service.Interfaces
{
    public interface IAuthenticationService
    {
        bool Authenticate(string username, string password);
    }
}