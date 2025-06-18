using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyChatBackend.Models.Account;

public record SysSettingDto(
    [property: Required, JsonPropertyName("maxGroupCount")]
    int MaxGroupCount = 5,
    [property: Required, JsonPropertyName("maxGroupMemberCount")]
    int MaxGroupMemberCount = 500,
    [property: Required, JsonPropertyName("maxImageSize")]
    int MaxImageSize = 2,
    [property: Required, JsonPropertyName("maxVideoSize")]
    int MaxVideoSize = 5,
    [property: Required, JsonPropertyName("maxFileSize")]
    int MaxFileSize = 5,
    [property: Required, JsonPropertyName("robotUid")]
    string RobotUid = "U_robot",
    [property: Required, JsonPropertyName("robotNickName")]
    string RobotNickName = "EasyChat",
    [property: Required, JsonPropertyName("robotWelcome")]
    string RobotWelcome = "欢迎使用EasyChat，期待与你的交流！"
);