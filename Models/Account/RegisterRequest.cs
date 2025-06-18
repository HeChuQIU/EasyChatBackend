using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyChatBackend.Models;

public record RegisterRequest(
    [property: Required]
    string CheckCodeKey,

    [property: Required, EmailAddress]
    string Email,

    [property: Required, DataType(DataType.Password)]
    string Password,

    [property: Required, StringLength(50, MinimumLength = 2), JsonPropertyName("nickName")]
    string Nickname,

    [property: Required]
    string CheckCode
);