# EasyChatBackend

## 项目简介
这个项目是[EasyChat后端](https://www.bilibili.com/video/BV1zT42127kM)的ASP.NET重写。

## 技术栈
- ASP.NET 9.0
- Entity Framework Core 9.0
- SQL Server
- RESTful API 设计
- JWT 认证与授权
- Docker 支持

## 项目结构
- Controllers：接口层，负责处理 HTTP 请求。
- Models：数据模型与 DTO。
- Services：业务逻辑层。
- Filters：自定义过滤器。
- Exceptions：异常处理。
- Common/Options：通用工具类与配置项。
- Migrations：数据库迁移。

## 快速开始
1. 安装 .NET 9.0 SDK 和与 EF Core 兼容的任意数据库。
2. 在`appsettings.json`或者用户机密中配置数据库连接字符串`ConnectionStrings:AccountConnection`。
3. 数据库迁移与初始化：
   ```bash
   dotnet ef database update
   ```
4. 启动项目：
   ```bash
   dotnet run
   ```
   