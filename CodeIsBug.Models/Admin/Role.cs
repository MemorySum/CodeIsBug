
namespace CodeIsBug.Models.Admin;

/// <summary>
/// 角色表
///</summary>
[Table(Name =("E_Sys_Role"))]
public class Role : BaseModel
{
    /// <summary>
    /// 角色名称 
    ///</summary>
   [Column(Name  = "Name")]
    public string Name { get; set; }
       
    /// <summary>
    /// 角色代码 
    ///</summary>
   [Column(Name  = "Code")]
    public string Code { get; set; }
    /// <summary>
    /// 角色描述 
    ///</summary>
   [Column(Name  = "Description")]
    public string Description { get; set; }
    /// <summary>
    /// 是否启用 
    ///</summary>
   [Column(Name  = "Enabled")]
    public int? Enabled { get; set; }
    /// <summary>
    /// 排序 
    ///</summary>
   [Column(Name  = "Sort")]
    public int? Sort { get; set; }
}