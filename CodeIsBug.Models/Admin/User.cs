using CodeIsBug.Models.Models;
using SqlSugar;

namespace CodeIsBug.Models.Admin;

/// <summary>
/// 用户表
///</summary>
[SugarTable(TableName =("E_Sys_User"))]
public class User : BaseModel
{
    /// <summary>
    /// 用户名 
    ///</summary>
   [SugarColumn(ColumnName = "UserName")]
    public string UserName { get; set; }
   
    /// <summary>
    /// 密码 
    ///</summary>
   [SugarColumn(ColumnName = "Password")]
    public string Password { get; set; }
    /// <summary>
    /// 昵称 
    ///</summary>
   [SugarColumn(ColumnName = "NickName")]
    public string NickName { get; set; }
    /// <summary>
    /// 头像 
    ///</summary>
   [SugarColumn(ColumnName = "Avatar")]
    public string Avatar { get; set; }
    /// <summary>
    /// 状态 
    ///</summary>
   [SugarColumn(ColumnName = "Status")]
    public int Status { get; set; }
    /// <summary>
    /// 备注 
    ///</summary>
   [SugarColumn(ColumnName = "Remark")]
    public string Remark { get; set; }
}