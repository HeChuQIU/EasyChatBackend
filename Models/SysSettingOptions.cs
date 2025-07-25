using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyChatBackend.Models;

public class SysSetting
{
    public const string Key = "SysSetting";

    [JsonPropertyName("maxGroupCount")]
    public int MaxGroupCount
    {
        get;
        set;
    }

    [JsonPropertyName("maxGroupMemberCount")]
    public int MaxGroupMemberCount
    {
        get;
        set;
    }

    [JsonPropertyName("maxImageSize")]
    public int MaxImageSize
    {
        get;
        set;
    }

    [JsonPropertyName("maxVideoSize")]
    public int MaxVideoSize
    {
        get;
        set;
    }

    [JsonPropertyName("maxFileSize")]
    public int MaxFileSize
    {
        get;
        set;
    }

    [JsonPropertyName("robotUid")]
    public string RobotUid
    {
        get;
        set;
    }

    [JsonPropertyName("robotNickName")]
    public string RobotNickName
    {
        get;
        set;
    }

    [JsonPropertyName("robotWelcome")]
    public string RobotWelcome
    {
        get;
        set;
    }

    public void Deconstruct(out int MaxGroupCount, out int MaxGroupMemberCount, out int MaxImageSize,
        out int MaxVideoSize, out int MaxFileSize, out string RobotUid, out string RobotNickName,
        out string RobotWelcome)
    {
        MaxGroupCount = this.MaxGroupCount;
        MaxGroupMemberCount = this.MaxGroupMemberCount;
        MaxImageSize = this.MaxImageSize;
        MaxVideoSize = this.MaxVideoSize;
        MaxFileSize = this.MaxFileSize;
        RobotUid = this.RobotUid;
        RobotNickName = this.RobotNickName;
        RobotWelcome = this.RobotWelcome;
    }
}