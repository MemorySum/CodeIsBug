using CodeIsBug.Models.Models;
using SqlSugar;

namespace CodeIsBug.Models.Admin;

/// <summary>
/// 字典表
///</summary>
[SugarTable(TableName ="E_Base_Dictionary")]
public class Dictionary:BaseModel
{
    
    /// <summary>
    /// 字典类型Id 
    ///</summary>
    [SugarColumn(ColumnName = "DictionaryTypeId",IsNullable =true)]
    public Guid DictionaryTypeId { get; set; }
    /// <summary>
    /// 名称 
    ///</summary>
    [SugarColumn(ColumnName= "Name", IsNullable = true)]
    public string Name { get; set; }
    /// <summary>
    /// 编码 
    ///</summary>
    [SugarColumn(ColumnName=  "Code", IsNullable = true)]
    public string Code { get; set; }
    /// <summary>
    /// 字典值 
    ///</summary>
    [SugarColumn(ColumnName=  "Value", IsNullable = true)]
    public string Value { get; set; }
    /// <summary>
    /// 描述 
    ///</summary>
    [SugarColumn(ColumnName=  "Description", IsNullable = true)]
    public string Description { get; set; }
    
}
