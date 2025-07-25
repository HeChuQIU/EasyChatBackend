using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

/// <summary>
/// ����״̬
/// </summary>
public enum ContactApplyStatusEnum
{
    Pending = 0, // ������
    Accepted = 1, // ��ͬ��
    Rejected = 2, // �Ѿܾ�
    Blocked = 3 // ������
}

[Index(nameof(ApplyUserId), Name = "idx_apply_user_id")]
[Index(nameof(ReceiveUserId), Name = "idx_receive_user_id")]
[Index(nameof(ContactId), Name = "idx_contact_id")]
public class UserContactApply
{
    /// <summary>
    /// ����ID
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ApplyId
    {
        get;
        set;
    }

    /// <summary>
    /// ������id
    /// </summary>
    [StringLength(12)]
    public string? ApplyUserId
    {
        get;
        set;
    }

    /// <summary>
    /// ������ID
    /// </summary>
    [StringLength(12)]
    public string? ReceiveUserId
    {
        get;
        set;
    }

    /// <summary>
    /// ��ϵ������ 0���� 1Ⱥ��
    /// </summary>
    public ContactTypeEnum? ContactType
    {
        get;
        set;
    }

    /// <summary>
    /// ��ϵ��Ⱥ��ID
    /// </summary>
    [StringLength(12)]
    public string? ContactId
    {
        get;
        set;
    }

    /// <summary>
    /// �������ʱ�䣨Unixʱ�������λ�����룩
    /// </summary>
    public long? LastApplyTime
    {
        get;
        set;
    }

    /// <summary>
    /// ״̬ 0������ 1:��ͬ�� 2:�Ѿܾ� 3:������
    /// </summary>
    public ContactApplyStatusEnum? Status
    {
        get;
        set;
    }

    /// <summary>
    /// ������Ϣ
    /// </summary>
    [StringLength(100)]
    public string? ApplyInfo
    {
        get;
        set;
    }
}