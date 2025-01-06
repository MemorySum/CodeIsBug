using Autofac;
using Autofac.Extensions.DependencyInjection;
using CodeIsBug.Api.Middleware;
using CodeIsBug.Common;
using CodeIsBug.Common.Components.Authorization;
using CodeIsBug.Common.Components.Cache;
using CodeIsBug.Common.Components.Configuration;
using CodeIsBug.Common.Components.DependencyInjection;
using CodeIsBug.Common.Components.JsonOptions.SystemTextJson;
using CodeIsBug.Common.Components.FreeSqlSetup;
using CodeIsBug.Common.Middleware;
using CodeIsBug.Common.Swagger;
using Microsoft.AspNetCore.HttpOverrides;
using CodeIsBug.Common.Components.Jwt;

var builder = WebApplication.CreateSlimBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
                            .Build();
//进行配置注册 | 添加静态文件读取(优先级比较高)
AppSettings.AddConfigSteup(configuration);
builder.Services.AddCacheSetup();

//基础服务注册
builder.Services.AddBaseServices();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new AutofacModuleRegister());
    });
// 添加过滤器
builder.Services.AddControllers();
   

builder.Services.AddEndpointsApiExplorer();
// 配置Json选项
builder.Services.AddTextJsonOptions();

// 添加FreeSql
builder.Services.AddFreeSql();

// 添加jwt认证
builder.Services.AddJwtSetup();
// 添加swagger文档
builder.Services.AddSwaggerBaseSetup();


//添加授权
//builder.Services.AddAuthorization();
// 添加自定义授权
builder.Services.AddAuthorizationSetup();

// 添加跨域支持
builder.Services.AddCorsSetup();

//响应缓存中间件
builder.Services.AddResponseCaching();

var app = builder.Build();

//写入静态类供全局获取
App.ServiceProvider = app.Services;
App.Configuration = builder.Configuration;

//ForwardedHeaders中间件会自动把反向代理服务器转发过来的X-Forwarded-For（客户端真实IP）以及X-Forwarded-Proto（客户端请求的协议）
//自动填充到HttpContext.Connection.RemoteIPAddress和HttpContext.Request.Scheme中，这样应用代码中读取到的就是真实的IP和真实的协议了，不需要应用做特殊处理。
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

//if (app.Environment.IsDevelopment()) 
// Configure the HTTP request pipeline.
if (AppSettings.DisplaySwaggerDoc) app.UseSwaggerExtension();

app.UseRouting(); // 确定路由
app.UseMiddleware<CheckToken>(); //身份验证中间件 

//app.UseMiddleware<GlobalExceptionMiddleware>(); //全局异常中间件 

app.UseDefaultFiles(); // 提供默认文件支持

app.UseStaticFiles(); // 启用静态文件服务

app.UseHttpsRedirection(); // 放在前面，确保所有请求都通过HTTPS


app.UseCors(); // 配置跨域资源共享

app.UseAuthentication(); // 启用身份验证中间件

app.UseAuthorization(); // 启用授权中间件

app.UseResponseCaching(); // 应用响应缓存

// app.MapHub<OnlineUserHub>("/hub"); // 映射SignalR Hub

app.MapControllers(); // 映射控制器

app.Run(); // 启动服务器