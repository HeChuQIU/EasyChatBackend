using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

/// <summary>
/// 申请状态
/// </summary>
public enum ContactApplyStatusEnum
{
    Pending = 0, // 待处理
    Accepted = 1, // 已同意
    Rejected = 2, // 已拒绝
    Blocked = 3 // 已拉黑
}

[Index(nameof(ApplyUserId), Name = "idx_apply_user_id")]
[Index(nameof(ReceiveUserId), Name = "idx_receive_user_id")]
[Index(nameof(ContactId), Name = "idx_contact_id")]
public class UserContactApply
{
    /// <summary>
    /// 自增ID
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ApplyId
    {
        get;
        set;
    }

    /// <summary>
    /// 申请人id
    /// </summary>
    [StringLength(12)]
    public string? ApplyUserId
    {
        get;
        set;
    }

    /// <summary>
    /// 接收人ID
    /// </summary>
    [StringLength(12)]
    public string? ReceiveUserId
    {
        get;
        set;
    }

    /// <summary>
    /// 联系人类型 0好友 1群组
    /// </summary>
    public ContactTypeEnum? ContactType
    {
        get;
        set;
    }

    /// <summary>
    /// 联系人群组ID
    /// </summary>
    [StringLength(12)]
    public string? ContactId
    {
        get;
        set;
    }

    /// <summary>
    /// 最后申请时间（Unix时间戳，单位：毫秒）
    /// </summary>
    public long? LastApplyTime
    {
        get;
        set;
    }

    /// <summary>
    /// 状态 0待处理 1:已同意 2:已拒绝 3:已拉黑
    /// </summary>
    public ContactApplyStatusEnum? Status
    {
        get;
        set;
    }

    /// <summary>
    /// 申请信息
    /// </summary>
    [StringLength(100)]
    public string? ApplyInfo
    {
        get;
        set;
    }
}