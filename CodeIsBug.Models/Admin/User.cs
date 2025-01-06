using CodeIsBug.Models.Models;
using FreeSql.DataAnnotations;

namespace CodeIsBug.Models.Admin;

/// <summary>
/// 用户表
///</summary>
[Table(Name = "E_Sys_User")]
public class User : BaseModel
{
    /// <summary>
    /// 用户名 
    ///</summary>
    [Column(Name = "UserName")]
    public string UserName { get; set; }

    /// <summary>
    /// 密码 
    ///</summary>
    [Column(Name  = "Password")]
    public string Password { get; set; }

    /// <summary>
    /// 昵称 
    ///</summary>
    [Column(Name  = "NickName")]
    public string NickName { get; set; }

    /// <summary>
    /// 头像 
    ///</summary>
    [Column(Name  = "Avatar")]
    public string Avatar { get; set; }

    /// <summary>
    /// 状态 
    ///</summary>
    [Column(Name  = "Status")]
    public int Status { get; set; }

    /// <summary>
    /// 备注 
    ///</summary>
    [Column(Name  = "Remark")]
    public string Remark { get; set; }

}