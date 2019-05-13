namespace Poker.Model.User
{
    public enum ValidationError
    {
        UsernameMissing,
        UsernameExists,
        UsernameToLong,
        PasswordMissing,
        PasswordToLong,
        FirstnameMissing,
        FirstnameToLong,
        LastnameMissing,
        LastnameToLong,
        EmailMissing,
        EmailToLong,
        EmailWrongFormat
    }
}