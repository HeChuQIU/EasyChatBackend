using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
    }

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
}