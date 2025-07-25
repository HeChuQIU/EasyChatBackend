using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

/// <summary>
/// 群组加入方式
/// </summary>
public enum GroupJoinTypeEnum
{
    DirectJoin = 0, // 直接加入
    ApprovalRequired = 1 // 管理员同意后加入
}

/// <summary>
/// 群组状态
/// </summary>
public enum GroupStatusEnum
{
    Dissolved = 0, // 解散
    Normal = 1 // 正常
}

[Index(nameof(GroupId), Name = "idx_key_groupid", IsUnique = true)]
public record GroupInfo(
    [Required]
    [property: Key, StringLength(12)]
    string GroupId,

    [property: StringLength(20)]
    string? GroupName = null,

    [property: StringLength(12)]
    string? GroupOwnerId = null,

    [property: DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    DateTime? CreateTime = null,

    [property: StringLength(500)]
    string? GroupNotice = null,

    GroupJoinTypeEnum? JoinType = GroupJoinTypeEnum.ApprovalRequired,

    GroupStatusEnum Status = GroupStatusEnum.Normal,

    byte[]? AvatarFile = null,

    byte[]? AvatarCover = null
);