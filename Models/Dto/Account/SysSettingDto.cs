using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyChatBackend.Models.Dto.Account;

public record SysSettingDto(
    [Required]
    [property: JsonPropertyName("maxGroupCount")]
    int MaxGroupCount = 5,
    [Required]
    [property: JsonPropertyName("maxGroupMemberCount")]
    int MaxGroupMemberCount = 500,
    [Required]
    [property: JsonPropertyName("maxImageSize")]
    int MaxImageSize = 2,
    [Required]
    [property: JsonPropertyName("maxVideoSize")]
    int MaxVideoSize = 5,
    [Required]
    [property: JsonPropertyName("maxFileSize")]
    int MaxFileSize = 5,
    [Required]
    [property: JsonPropertyName("robotUid")]
    string RobotUid = "U_robot",
    [Required]
    [property: JsonPropertyName("robotNickName")]
    string RobotNickName = "EasyChat",
    [Required]
    [property: JsonPropertyName("robotWelcome")]
    string RobotWelcome = "欢迎使用EasyChat，期待与你的交流！"
);