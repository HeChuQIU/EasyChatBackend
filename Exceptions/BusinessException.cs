namespace EasyChatBackend.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message)
    {
        Message = message;
    }

    public string? Code
    {
        get;
        set;
    }

    public string Message
    {
        get;
        set;
    }
}