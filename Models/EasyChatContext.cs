using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

public class EasyChatContext(DbContextOptions<EasyChatContext> options) : DbContext(options)
{
    public DbSet<UserInfo> Users
    {
        get;
        set;
    }

    public DbSet<UserInfoBeauty> UserBeauties
    {
        get;
        set;
    }

    public DbSet<GroupInfo> Groups
    {
        get;
        set;
    }

    public DbSet<UserContact> UserContacts
    {
        get;
        set;
    }

    public DbSet<UserContactApply> UserContactApplies
    {
        get;
        set;
    }
}