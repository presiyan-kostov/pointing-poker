namespace Poker.Service.Interfaces
{
    public interface IAuthenticationService
    {
        int? Authenticate(string username, string password);
    }
}