using CodeIsBug.Models.Models;
using SqlSugar;

namespace CodeIsBug.Models.Admin;

/// <summary>
/// 角色权限对照表
///</summary>
[SugarTable(TableName =("E_Sys_RolePermissionMap"))]
public class RolePermissionMap : BaseModel
{
    /// <summary>
    /// 1 
    ///</summary>
   [SugarColumn(ColumnName = "RoleId")]
    public Guid RoleId { get; set; }
    /// <summary>
    ///  
    ///</summary>
   [SugarColumn(ColumnName = "PermissionId")]
    public Guid PermissionId { get; set; }
}