using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

/// <summary>
/// ��ϵ������
/// </summary>
public enum ContactTypeEnum
{
    Friend = 0, // ����
    Group = 1   // Ⱥ��
}

/// <summary>
/// ��ϵ��״̬
/// </summary>
public enum ContactStatusEnum
{
    NotFriend = 0,      // �Ǻ���
    Friend = 1,         // ����
    Deleted = 2,        // ��ɾ������
    DeletedBy = 3,      // ������ɾ��
    Blocked = 4,        // �����ں���
    BlockedBy = 5       // ����������
}

[PrimaryKey(nameof(UserId), nameof(ContactId))]
[Index(nameof(ContactId), Name = "idx_contact_id")]
public class UserContact
{
    /// <summary>
    /// �û�ID
    /// </summary>
    [Required]
    [StringLength(12)]
    public string UserId
    {
        get; set;
    }

    /// <summary>
    /// ��ϵ��ID����Ⱥ��ID
    /// </summary>
    [Required]
    [StringLength(12)]
    public string ContactId
    {
        get; set;
    }

    /// <summary>
    /// ��ϵ������ 0:���� 1:Ⱥ��
    /// </summary>
    public ContactTypeEnum? ContactType
    {
        get; set;
    }

    /// <summary>
    /// ����ʱ��
    /// </summary>
    public DateTime? CreateTime
    {
        get; set;
    }

    /// <summary>
    /// ״̬
    /// </summary>
    public ContactStatusEnum? Status
    {
        get; set;
    }

    /// <summary>
    /// ������ʱ��
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? LastUpdateTime
    {
        get; set;
    }
}