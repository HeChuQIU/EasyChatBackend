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
            logger.LogInformation("׼���������ݿ�");

            context.Users.AddRange(
                new UserInfo
                {
                    UserId = "user001",
                    Email = "user001@example.com",
                    Nickname = "С��",
                    JoinType = JoinTypeEnum.DirectJoin,
                    Sex = SexEnum.Male,
                    Password = accountService.EncodePassword("12345678@a"),
                    PersonalSignature = "�Ȱ�����",
                    Status = StatusEnum.Active,
                    CreateTime = DateTime.UtcNow,
                    LastLoginTime = DateTime.UtcNow,
                    AreaName = "����",
                    AreaCode = "010",
                    LastOffTime = DateTime.UtcNow
                },
                new UserInfo
                {
                    UserId = "user002",
                    Email = "user002@example.com",
                    Nickname = "С��",
                    JoinType = JoinTypeEnum.ApprovalRequired,
                    Sex = SexEnum.Female,
                    Password = accountService.EncodePassword("password2@"),
                    PersonalSignature = "ϲ������",
                    Status = StatusEnum.Active,
                    CreateTime = DateTime.UtcNow,
                    LastLoginTime = DateTime.UtcNow,
                    AreaName = "�Ϻ�",
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
            logger.LogInformation("���ݿⲥ�����");
        }
        else
        {
            logger.LogInformation("���ݿ��Ѵ������ݣ����貥��");
        }
    }
}