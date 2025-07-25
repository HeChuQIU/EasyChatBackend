using System.ComponentModel.DataAnnotations;

namespace EasyChatBackend.Models.Dto.Account;

public record RegisterRequestDto(
    [Required] string CheckCodeKey,
    [Required] [EmailAddress] string Email,
    [Required]
    [DataType(DataType.Password)]
    string Password,
    [Required]
    [StringLength(50, MinimumLength = 2)]
    string Nickname,
    [Required] string CheckCode
);