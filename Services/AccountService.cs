using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;
using EasyChatBackend.Common;
using EasyChatBackend.Models;
using EasyChatBackend.Models.Dto.Account;
using EasyChatBackend.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EasyChatBackend.Services;

public class AccountService(IOptions<JwtOptions> jwtOptions, EasyChatContext context, Random random)
{
    private readonly JwtOptions _jwt = jwtOptions.Value;

    public Result<UserInfo> Register(string email, string nickname, string password)
    {
        // 显式开启事务，确保多表操作的原子性
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var userInfo = context.Users.FirstOrDefault(u => u.Email == email);
            if (userInfo is not null)
            {
                return Result.Failure<UserInfo>("邮箱已存在");
            }

            string userId;
            var beautyAccount = context.UserBeauties.FirstOrDefault(u => u.Email == email);
            var beautyAccountAvailable = beautyAccount?.Status is StatusEnum.Inactive;
            if (beautyAccountAvailable)
            {
                userId = beautyAccount.UserId;
                beautyAccount.Status = StatusEnum.Active;
            }
            else
            {
                userId = GenerateUnUsedUserId();
            }

            var currentTime = DateTime.UtcNow;

            userInfo = new UserInfo
            {
                UserId = userId,
                Nickname = nickname,
                Email = email,
                Password = EncodePassword(password),
                CreateTime = currentTime,
                Status = StatusEnum.Active,
                LastOffTime = currentTime,
                JoinType = JoinTypeEnum.DirectJoin
            };

            context.Users.Add(userInfo);
            context.SaveChanges();

            transaction.Commit();
            return Result.Success(userInfo);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            // 可记录日志 ex
            return Result.Failure<UserInfo>("注册失败，请重试");
        }
    }

    public Result<LoginDto> Login(string email, string password, bool isPasswordEncored = true)
    {
        try
        {
            var encodedPassword = isPasswordEncored ? password : EncodePassword(password);
            var userInfo = context.Users.FirstOrDefault(u => u.Email == email && u.Password == encodedPassword);
            var isAdmin = true;

            if (userInfo?.Status is StatusEnum.Banned)
            {
                return Result.Failure<LoginDto>("账号已被禁用，请联系管理员");
            }

            if (userInfo is null)
            {
                return Result.Failure<LoginDto>("账号或密码错误");
            }

            userInfo.LastLoginTime = DateTime.UtcNow;

            var claims = new List<Claim>();

            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var token = GenerateToken(userInfo, claims);
            var loginDto = LoginDto.Create(userInfo, token, isAdmin);

            context.SaveChanges();  

            return Result.Success(loginDto);
        }
        catch (Exception ex)
        {
            // 可记录日志 ex
            return Result.Failure<LoginDto>("登录失败，请重试");
        }
    }

    public string GenerateToken(UserInfo userInfo, params List<Claim> additionalClaims)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, userInfo.UserId),
            new(ClaimTypes.Email, userInfo.Email),
            new(ClaimTypes.Name, userInfo.Nickname)
        }.Concat(additionalClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.ExpireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateUnUsedUserId()
    {
        string userId;
        do
        {
            userId = IdPrefixes.User + GenerateUserId();
        } while (IsIdUsed(userId));

        return userId;
    }

    public string GenerateUserId()
    {
        return IdPrefixes.User + string.Concat(Enumerable.Range(0, 11).Select(_ => random.Next(0, 10).ToString()));
    }

    public string EncodePassword(string password)
    {
        using var md5 = MD5.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = md5.ComputeHash(bytes);
        var sb = new StringBuilder();
        foreach (var b in hash)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    public bool IsIdUsed(string userId) =>
        context.Users.Any(u => u.UserId == userId) ||
        context.UserBeauties.Any(u => u.UserId == userId);
}