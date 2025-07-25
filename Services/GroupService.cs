using System;
using CSharpFunctionalExtensions;
using EasyChatBackend.Common;
using EasyChatBackend.Models;

namespace EasyChatBackend.Services;

public class GroupService(SysSetting sysSetting, EasyChatContext context, Random random)
{
    public async Task<Result> SaveGroup(GroupInfo groupInfo, IFormFile avatarFile, IFormFile avatarCover)
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            if (string.IsNullOrEmpty(groupInfo.GroupId))
            {
                var existedGroupCount = context.Groups.Count(info => info.GroupOwnerId == groupInfo.GroupOwnerId);
                if (existedGroupCount >= sysSetting.MaxGroupCount)
                {
                    return Result.Failure($"最多只能创建{sysSetting.MaxGroupCount}个群组");
                }

                var currentTime = DateTime.Now;

                groupInfo = groupInfo with
                {
                    CreateTime = currentTime,
                    GroupId = GenerateUnusedGroupId(),
                };

                context.Groups.Add(groupInfo);

                var userContact = new UserContact
                {
                    Status = ContactStatusEnum.Friend,
                    ContactType = ContactTypeEnum.Group,
                    ContactId = groupInfo.GroupId,
                    UserId = groupInfo.GroupOwnerId!,
                    CreateTime = currentTime
                };

                context.UserContacts.Add(userContact);

                using var avatarStream = new MemoryStream();
                await avatarFile.CopyToAsync(avatarStream);
                groupInfo = groupInfo with
                {
                    AvatarFile = avatarStream.ToArray()
                };

                using var avatarCoverStream = new MemoryStream();
                await avatarCover.CopyToAsync(avatarCoverStream);
                groupInfo = groupInfo with
                {
                    AvatarCover = avatarCoverStream.ToArray()
                };
            }
            else
            {
                if (context.Groups.FirstOrDefault(info => info.GroupId == groupInfo.GroupId)?.GroupOwnerId !=
                    groupInfo.GroupOwnerId)
                {
                    return Result.Failure("只能修改自己创建的群组");
                }

                var existingGroup = context.Groups.FirstOrDefault(info => info.GroupId == groupInfo.GroupId);
                if (existingGroup == null)
                {
                    return Result.Failure("群组不存在");
                }
            }

            transaction.Commit();
            context.SaveChanges();
            return Result.Success();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return Result.Failure($"保存群组失败: {ex.Message}");
        }
    }

    public bool IsGroupIdUsed(string groupId)
    {
        return context.Groups.Any(info => info.GroupId == groupId);
    }

    public string GenerateGroupId()
    {
        return IdPrefixes.Group + string.Concat(Enumerable.Range(0, 11).Select(_ => random.Next(0, 10).ToString()));
    }

    public string GenerateUnusedGroupId()
    {
        string groupId;
        do
        {
            groupId = GenerateGroupId();
        } while (IsGroupIdUsed(groupId));

        return groupId;
    }
}