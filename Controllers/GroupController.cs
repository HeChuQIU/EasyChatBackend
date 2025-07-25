using System.Security.Claims;
using EasyChatBackend.Filters;
using EasyChatBackend.Models;
using EasyChatBackend.Models.Dto.GroupInfo;
using EasyChatBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyChatBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
[BlockExpiredJwtAsyncActionFilter]
[Authorize]
public class GroupController(GroupService groupService) : ControllerBase
{
    [HttpPost("saveGroup")]
    public async Task<ActionResult<ResponseVo>> SaveGroup([FromForm] SaveGroupRequestDto saveGroupRequestDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var (groupId, groupName, groupNotice, groupJoinTypeEnum, avatarFile, avatarCover) = saveGroupRequestDto;
        var groupInfo = new GroupInfo(
            GroupId: groupId,
            GroupOwnerId: userId,
            GroupName: groupName,
            GroupNotice: groupNotice,
            JoinType: groupJoinTypeEnum);

        groupService.SaveGroup(groupInfo, avatarFile, avatarCover);
        return Ok();
    }
}