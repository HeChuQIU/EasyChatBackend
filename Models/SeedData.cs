using EasyChatBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace EasyChatBackend.Models;

public class SeedData(EasyChatContext context, AccountService accountService, ILogger<SeedData> logger)
{
    public void SeedDatabase()
    {
        context.Database.Migrate();
        if (!context.UserBeauties.Any())
        {
            logger.LogInformation("准备播种数据库");

            context.Users.AddRange(
                new UserInfo
                {
                    UserId = "user001",
                    Email = "user001@example.com",
                    Nickname = "小明",
                    JoinType = JoinTypeEnum.DirectJoin,
                    Sex = SexEnum.Male,
                    Password = accountService.EncodePassword("12345678@a"),
                    PersonalSignature = "热爱生活",
                    Status = StatusEnum.Active,
                    CreateTime = DateTime.UtcNow,
                    LastLoginTime = DateTime.UtcNow,
                    AreaName = "北京",
                    AreaCode = "010",
                    LastOffTime = DateTime.UtcNow
                },
                new UserInfo
                {
                    UserId = "user002",
                    Email = "user002@example.com",
                    Nickname = "小红",
                    JoinType = JoinTypeEnum.ApprovalRequired,
                    Sex = SexEnum.Female,
                    Password = accountService.EncodePassword("password2@"),
                    PersonalSignature = "喜欢旅行",
                    Status = StatusEnum.Active,
                    CreateTime = DateTime.UtcNow,
                    LastLoginTime = DateTime.UtcNow,
                    AreaName = "上海",
                    AreaCode = "021",
                    LastOffTime = DateTime.UtcNow
                }
            );

            context.UserBeauties.AddRange(
                new UserInfoBeauty
                {
                    UserId = "user001",
                    Email = "user001@example.com",
                    Status = StatusEnum.Active
                },
                new UserInfoBeauty
                {
                    UserId = "user002",
                    Email = "user002@example.com",
                    Status = StatusEnum.Inactive
                }
            );

            context.SaveChanges();
            logger.LogInformation("数据库播种完成");
        }
        else
        {
            logger.LogInformation("数据库已存在数据，无需播种");
        }
    }
}