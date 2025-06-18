using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EasyChatBackend.Models;

namespace EasyChatBackend.Models.Account;

public record LoginDto(
    [property: Required]
    [property: JsonPropertyName("userId")]
    string UserId,
    [property: Required]
    [property: JsonPropertyName("nickName")]
    string Nickname,
    [property: JsonPropertyName("sex")] SexEnum Sex,
    [property: JsonPropertyName("joinType")]
    JoinTypeEnum JoinType,
    [property: JsonPropertyName("personalSignature")]
    string PersonalSignature,
    [property: JsonPropertyName("areaCode")]
    string AreaCode,
    [property: JsonPropertyName("areaName")]
    string AreaName,
    [property: Required]
    [property: JsonPropertyName("token")]
    string Token,
    [property: Required]
    [property: JsonPropertyName("admin")]
    bool IsAdmin,
    [property: JsonPropertyName("status")] StatusEnum Status)
{
    public static LoginDto Create(UserInfo info, string token, bool isAdmin = false)
    {
        return new LoginDto(info.UserId,
            info.Nickname,
            info.Sex,
            info.JoinType,
            info.PersonalSignature,
            info.AreaCode,
            info.AreaName, token, isAdmin, info.Status);
    }
}