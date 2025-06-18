using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using EasyChatBackend.Common;
using EasyChatBackend.Exceptions;
using EasyChatBackend.Models;
using EasyChatBackend.Models.Account;
using EasyChatBackend.Options;
using EasyChatBackend.Services;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;

namespace EasyChatBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(
    AccountService accountService,
    ICaptcha captcha,
    HybridCache cache,
    IOptions<AccountOptions> options,
    ILogger<AccountController> logger) : ControllerBase
{
    private TimeSpan checkCodeExpiration = TimeSpan.FromMinutes(options.Value.CheckCodeExpirationMinutes);

    [HttpPost("checkCode")]
    public async Task<ActionResult<ResponseVo<CheckCodeDto>>> CheckCode()
    {
        var id = CacheKey.NewCaptchaKey();
        var info = captcha.Generate(id);
        cache.SetAsync(id, info.Code,
            new HybridCacheEntryOptions()
            {
                Expiration = checkCodeExpiration
            });
        logger.LogInformation("验证码是：{code}", info.Code);

        var response = new CheckCodeDto(
            info.Base64,
            id
        );
        return Ok(ResponseVo<CheckCodeDto>.Success(response));
    }

    [HttpPost("register")]
    public async Task<ActionResult<ResponseVo>> Register(
        [FromForm] [Required] string checkCodeKey,
        [FromForm] [Required, EmailAddress] string email,
        [FromForm] [Required, DataType(DataType.Password)]
        string password,
        [FromForm] [Required, StringLength(50, MinimumLength = 2)]
        string nickname,
        [FromForm] [Required] string checkCode)
    {
        var cachedCode =
            await cache.GetOrCreateAsync<string?>(checkCodeKey, _ => ValueTask.FromResult<string?>(null));

        if (checkCode != cachedCode)
        {
            logger.LogWarning("验证码不匹配: {CheckCode} != {CachedCode}", checkCode, cachedCode);
            return ResponseVo.BusinessError("验证码不正确");
        }

        await cache.RemoveAsync(checkCodeKey);

        return ResponseVo.Success();
    }

    [HttpPost("login")]
    public async Task<ActionResult<ResponseVo>> Login(
        [FromForm] [Required] string checkCodeKey,
        [FromForm] [Required, EmailAddress] string email,
        [FromForm] [Required] string password,
        [FromForm] [Required] string checkCode)
    {
        var cachedCode =
            await cache.GetOrCreateAsync<string?>(checkCodeKey, _ => ValueTask.FromResult<string?>(null));
        await cache.RemoveAsync(checkCodeKey);

        if (checkCode != cachedCode)
        {
            logger.LogWarning("验证码不匹配: {CheckCode} != {CachedCode}", checkCode, cachedCode);
            return ResponseVo.BusinessError("验证码不正确");
        }

        var loginResult = accountService.Login(email, password);
        if (loginResult.IsSuccess)
        {
            return ResponseVo<LoginDto>.Success(loginResult.Value);
        }

        logger.LogWarning("登录失败: {ErrorMessage}", loginResult.Error);
        return ResponseVo.BusinessError(loginResult.Error);
    }

    [HttpPost("getSysSetting")]
    public async Task<ActionResult<ResponseVo<SysSettingDto>>> GetSysSetting()
    {
        var setting = new SysSettingDto();
        return Ok(ResponseVo<SysSettingDto>.Success(setting));
    }
}