using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

[Index(nameof(UserId), Name = "idx_key_userid", IsUnique = true)]
[Index(nameof(Email), Name = "idx_key_email", IsUnique = true)]
public class UserInfoBeauty
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
        get;
        set;
    }

    [Required]
    [StringLength(12)]
    public string UserId
    {
        get;
        set;
    }

    [Required]
    [StringLength(50)]
    public string Email
    {
        get;
        set;
    }

    public StatusEnum Status
    {
        get;
        set;
    }
}