using CodeIsBug.Common.Components.API_Resultss.Helper;
using Microsoft.Extensions.Configuration;

namespace CodeIsBug.Common.Components.Configuration;

/// <summary>
/// 全局appsetting静态类
/// </summary>
public static class AppSettings
{
    private static IConfiguration? _configuration;

    /// <summary>
    /// 配置器构造函数
    /// </summary>
    /// <exception cref="NullReferenceException"></exception>
    public static IConfiguration Configuration
    {
        get
        {
            if (_configuration == null) throw new NullReferenceException(nameof(Configuration));
            return _configuration;
        }
    }


    /// <summary>
    /// 设置 Configuration 的实例
    /// </summary>
    /// <param name="configuration"></param>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddConfigSteup(IConfiguration? configuration)
    {
        if (_configuration != null) throw ResultHelper.Exception207Bad($"{nameof(Configuration)}不可修改！");
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    #region 以下存放的全部都是静态配置

    /// <summary>
    /// 允许跨域请求列表
    /// </summary>
    public static string[] AllowCors => Configuration.GetSection("AllowCors").Get<string[]>() ?? [];

    /// <summary>
    /// 是否演示环境
    /// </summary>
    public static bool IsDemo => Convert.ToBoolean(Configuration["IsDemo"]);

    /// <summary>
    /// 是否展示swaggeer文档
    /// </summary>
    public static bool DisplaySwaggerDoc => Convert.ToBoolean(Configuration["DisplaySwaggerDoc"]);

    /// <summary>
    /// 数据库链接
    /// </summary>
    public static string ConnectionString => _configuration!.GetConnectionString("CodeIsBug.DB") ?? "";


    /// <summary>
    /// RabbitMq链接
    /// </summary>
    public static string RabbitqConnetion => Configuration["RabbitMqConnetion"] ?? "";


    /// <summary>
    /// Jwt 配置
    /// </summary>
    public static class Jwt
    {
        public static string SecretKey => Configuration["Jwt:SecretKey"] ?? "";
        public static string Issuer => Configuration["Jwt:Issuer"] ?? "";
        public static string Audience => Configuration["Jwt:Audience"] ?? "";
    }


    /// <summary>
    /// Redis 配置
    /// </summary>
    public static class Redis
    {
        public static bool Enabled => Configuration.GetValue<bool>("Redis:Enabled");
        public static string ConnectionString => Configuration["Redis:ConnectionString"] ?? "ConnectionStringError";
        
        public static string Instance => Configuration["Redis:Instance"] ?? "Default";
    }

    #endregion
}