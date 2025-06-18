namespace EasyChatBackend.Common;

public static class CacheKey
{
    public static string NewCaptchaKey()
    {
        return $"Captcha/{Guid.NewGuid():N}";
    }

    public static string NewUserHeartBeatKey()
    {
        return $"UserHeartBeat/{Guid.NewGuid():N}";
    }
}