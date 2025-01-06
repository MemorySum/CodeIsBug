
namespace CodeIsBug.Models.Admin;

/// <summary>
/// 用户角色对照表
///</summary>
[Table(Name =("E_Sys_UserRoleMap"))]
public class UserRoleMap : BaseModel
{
    /// <summary>
    /// 用户ID
    ///</summary>
    [Column(Name  = "UserId")]
    public Guid UserId { get; set; }
    /// <summary>
    ///  角色ID
    ///</summary>
    [Column(Name  = "RoleId")]
    public Guid RoleId { get; set; }    
}