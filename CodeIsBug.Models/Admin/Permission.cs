using CodeIsBug.Models.Enums;

namespace CodeIsBug.Models.Admin;

/// <summary>
/// 权限表
///</summary>
[Table(Name =("E_Sys_Permission"))]
public class Permission : BaseModel
{
    /// <summary>
    /// ParentId 
    ///</summary>
   [Column(Name  = "ParentId")]
    public Guid ParentId { get; set; }
   
    /// <summary>
    /// 权限名称 
    ///</summary>
   [Column(Name  = "Label")]
    public string Label { get; set; }
    /// <summary>
    /// 权限编码 
    ///</summary>
   [Column(Name  = "Code")]
    public string Code { get; set; }
    /// <summary>
    /// 权限类型 
    ///</summary>
   [Column(Name  = "Type")]
    public PermissionType? Type { get; set; }
    /// <summary>
    /// 菜单访问地址 
    ///</summary>
   [Column(Name  = "Path")]
    public string Path { get; set; }
    /// <summary>
    /// 图标 
    ///</summary>
   [Column(Name  = "Icon")]
    public string Icon { get; set; }
    /// <summary>
    /// 是否隐藏 
    ///</summary>
   [Column(Name  = "Hidden")]
    public bool Hidden { get; set; }
    /// <summary>
    /// 是否启用 
    ///</summary>
   [Column(Name  = "Enabled")]
    public bool Enabled { get; set; }
    /// <summary>
    /// 排序 
    ///</summary>
   [Column(Name  = "Sort")]
    public int? Sort { get; set; }
}
