using CodeIsBug.Models.Models;
using SqlSugar;

namespace CodeIsBug.Models.Admin;

/// <summary>
/// 角色表
///</summary>
[SugarTable(TableName =("E_Sys_Role"))]
public class Role : BaseModel
{
    /// <summary>
    /// 角色名称 
    ///</summary>
    [SugarColumn(ColumnName = "Name")]
    public string Name { get; set; }
       
    /// <summary>
    /// 角色代码 
    ///</summary>
    [SugarColumn(ColumnName = "Code")]
    public string Code { get; set; }
    /// <summary>
    /// 角色描述 
    ///</summary>
    [SugarColumn(ColumnName = "Description")]
    public string Description { get; set; }
    /// <summary>
    /// 是否启用 
    ///</summary>
    [SugarColumn(ColumnName = "Enabled")]
    public int? Enabled { get; set; }
    /// <summary>
    /// 排序 
    ///</summary>
    [SugarColumn(ColumnName = "Sort")]
    public int? Sort { get; set; }
}