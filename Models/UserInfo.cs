using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

public enum JoinTypeEnum
{
    DirectJoin = 0, // 直接加入
    ApprovalRequired = 1 // 同意后加好友
}

public enum SexEnum
{
    Female = 0, // 女
    Male = 1 // 男
}

public enum StatusEnum
{
    Active = 0, // 活跃
    Inactive = 1, // 不活跃
    Banned = 2 // 禁用
}

[Index(nameof(UserId), Name = "idx_key_userid", IsUnique = true)]
[Index(nameof(Email), Name = "idx_key_email", IsUnique = true)]
public class UserInfo
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Key]
    [Required]
    [StringLength(12)]
    public string UserId
    {
        get;
        set;
    }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Email
    {
        get;
        set;
    }

    /// <summary>
    /// 昵称
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Nickname
    {
        get;
        set;
    }

    /// <summary>
    /// 0:直接加入 1:同意后加好友
    /// </summary>
    public JoinTypeEnum JoinType
    {
        get;
        set;
    } = JoinTypeEnum.ApprovalRequired;

    /// <summary>
    /// 性别 0:女 1:男
    /// </summary>
    public SexEnum Sex
    {
        get;
        set;
    } = SexEnum.Male;

    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    [StringLength(32)]
    public string Password
    {
        get;
        set;
    }

    /// <summary>
    /// 个性签名
    /// </summary>
    [StringLength(255)]
    public string PersonalSignature
    {
        get;
        set;
    }

    /// <summary>
    /// 状态
    /// </summary>
    public StatusEnum Status
    {
        get;
        set;
    } = StatusEnum.Active;

    /// <summary>
    /// 创建时间
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreateTime
    {
        get;
        set;
    } = DateTime.UtcNow;

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime LastLoginTime
    {
        get;
        set;
    }

    /// <summary>
    /// 地区
    /// </summary>
    [StringLength(255)]
    public string AreaName
    {
        get;
        set;
    }

    /// <summary>
    /// 地区编号
    /// </summary>
    [StringLength(255)]
    public string AreaCode
    {
        get;
        set;
    }

    /// <summary>
    /// 最后离线时间
    /// </summary>
    public DateTime LastOffTime
    {
        get;
        set;
    }
}