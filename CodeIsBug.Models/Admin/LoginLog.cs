

namespace CodeIsBug.Models.Admin;

/// <summary>
/// 登录日志表
///</summary>
[Table(Name = ("E_Sys_LoginLog"))]
public class LoginLog : BaseModel
{
    /// <summary>
    /// 昵称 
    ///</summary>
    [Column(Name  = "NickName")]
    public string NickName { get; set; }
    /// <summary>
    /// IP地址 
    ///</summary>
    [Column(Name  = "IP")]
    public string IP { get; set; }
    /// <summary>
    /// 浏览器 
    ///</summary>
    [Column(Name  = "Browser")]
    public string Browser { get; set; }
    /// <summary>
    /// 系统 
    ///</summary>
    [Column(Name  = "OS")]
    public string OS { get; set; }
    /// <summary>
    /// 设备 
    ///</summary>
    [Column(Name  = "Device")]
    public string Device { get; set; }
    /// <summary>
    /// 浏览器信息 
    ///</summary>
    [Column(Name  = "BrowserInfo")]
    public string BrowserInfo { get; set; }
    /// <summary>
    /// 登录耗时 
    ///</summary>
    [Column(Name  = "ElapsedMilliseconds")]
    public int? ElapsedMilliseconds { get; set; }
    /// <summary>
    /// 登录结果 
    ///</summary>
    [Column(Name  = "Result")]
    public bool? Result { get; set; }
}
