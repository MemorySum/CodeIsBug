using CodeIsBug.Models.Models;
using SqlSugar;

namespace CodeIsBug.Models.Admin;

/// <summary>
/// 字典类型表
///</summary>
[SugarTable(TableName ="E_Base_DictionaryType")]
public class DictionaryType:BaseModel
{
    /// <summary>
    /// 名称 
    ///</summary>
   [SugarColumn(ColumnName= "Name")]
    public string Name { get; set; }
    /// <summary>
    /// 编码 
    ///</summary>
   [SugarColumn(ColumnName= "Code")]
    public string Code { get; set; }
    /// <summary>
    /// 描述 
    ///</summary>
   [SugarColumn(ColumnName= "Description")]
    public string Description { get; set; }
    /// <summary>
    /// 是否启用 
    ///</summary>
   [SugarColumn(ColumnName= "Enabled")]
    public bool Enabled { get; set; }
    /// <summary>
    /// 排序 
    ///</summary>
   [SugarColumn(ColumnName= "Sort")]
    public int? Sort { get; set; }
}
