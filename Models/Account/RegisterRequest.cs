using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyChatBackend.Models;

public record RegisterRequest(string CheckCodeKey, string Email, string Password, string Nickname, string CheckCode)
{
    [Required]
    public string CheckCodeKey
    {
        get;
        set;
    } = CheckCodeKey;

    [Required]
    [EmailAddress]
    public string Email
    {
        get;
        set;
    } = Email;

    [Required]
    [DataType(DataType.Password)]
    public string Password
    {
        get;
        set;
    } = Password;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    [JsonPropertyName("nickName")]
    public string Nickname
    {
        get;
        set;
    } = Nickname;

    [Required]
    public string CheckCode
    {
        get;
        set;
    } = CheckCode;
}