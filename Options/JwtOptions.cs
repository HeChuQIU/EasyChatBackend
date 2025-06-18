namespace EasyChatBackend.Options;

public class JwtOptions
{
    public const string Position = "Jwt";

    public string Issuer
    {
        get;
        set;
    } = string.Empty;

    public string Audience
    {
        get;
        set;
    } = string.Empty;

    public int ExpireMinutes
    {
        get;
        set;
    }

    public string SecretKey
    {
        get;
        set;
    } = string.Empty;
}