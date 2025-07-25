using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

/// <summary>
/// 联系人类型
/// </summary>
public enum ContactTypeEnum
{
    Friend = 0, // 好友
    Group = 1   // 群组
}

/// <summary>
/// 联系人状态
/// </summary>
public enum ContactStatusEnum
{
    NotFriend = 0,      // 非好友
    Friend = 1,         // 好友
    Deleted = 2,        // 已删除好友
    DeletedBy = 3,      // 被好友删除
    Blocked = 4,        // 已拉黑好友
    BlockedBy = 5       // 被好友拉黑
}

[PrimaryKey(nameof(UserId), nameof(ContactId))]
[Index(nameof(ContactId), Name = "idx_contact_id")]
public class UserContact
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required]
    [StringLength(12)]
    public string UserId
    {
        get; set;
    }

    /// <summary>
    /// 联系人ID或者群组ID
    /// </summary>
    [Required]
    [StringLength(12)]
    public string ContactId
    {
        get; set;
    }

    /// <summary>
    /// 联系人类型 0:好友 1:群组
    /// </summary>
    public ContactTypeEnum? ContactType
    {
        get; set;
    }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime
    {
        get; set;
    }

    /// <summary>
    /// 状态
    /// </summary>
    public ContactStatusEnum? Status
    {
        get; set;
    }

    /// <summary>
    /// 最后更新时间
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? LastUpdateTime
    {
        get; set;
    }
}