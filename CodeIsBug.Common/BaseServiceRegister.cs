using CodeIsBug.Common.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class BaseServiceRegister
{
    //注册项目所需的服务
    public static IServiceCollection AddBaseServices(this IServiceCollection services)
    { 
        //HttpContext
        services.AddHttpContextAccessor();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<ITokenService, TokenService>();

        return services;
    }
}
