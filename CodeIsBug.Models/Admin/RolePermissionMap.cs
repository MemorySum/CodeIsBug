
namespace CodeIsBug.Models.Admin;

/// <summary>
/// 角色权限对照表
///</summary>
[Table(Name =("E_Sys_RolePermissionMap"))]
public class RolePermissionMap : BaseModel
{
    /// <summary>
    /// 1 
    ///</summary>
   [Column(Name  = "RoleId")]
    public Guid RoleId { get; set; }
    /// <summary>
    ///  
    ///</summary>
   [Column(Name  = "PermissionId")]
    public Guid PermissionId { get; set; }
}