using System.ComponentModel.DataAnnotations;

namespace EasyChatBackend.Models.Dto.GroupInfo;

public record SaveGroupRequestDto(
    string? GroupId,
    [Required] string GroupName,
    string? GroupNotice,
    [Required] GroupJoinTypeEnum GroupJoinType,
    IFormFile? AvatarFile,
    IFormFile? AvatarCover);