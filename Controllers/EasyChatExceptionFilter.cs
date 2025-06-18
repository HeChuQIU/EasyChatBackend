using EasyChatBackend.Exceptions;
using EasyChatBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyChatBackend.Controllers;

public class EasyChatExceptionFilter(ILogger<EasyChatExceptionFilter> logger) : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        logger.LogError("请求错误，请求地址{url}，错误信息：{ex}", context.HttpContext.Request.Path, ex);
        var response = ex switch
        {
            BusinessException biz => ResponseVo.Error(biz.Code ?? "600", biz.Message),
            _ => ResponseVo.Error("500", "未知错误")
        };
        context.Result = new JsonResult(response);
    }
}