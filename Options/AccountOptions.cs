namespace EasyChatBackend.Options;

public class AccountOptions
{
    public const string Position = "Application:Account:CheckCodeExpirationMinutes";
    public int CheckCodeExpirationMinutes
    {
        get;
        set;
    } = 10;
}