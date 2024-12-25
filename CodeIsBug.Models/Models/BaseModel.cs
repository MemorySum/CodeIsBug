using SqlSugar;

namespace CodeIsBug.Models.Models;

/// <summary>
/// 实体通用属性
/// </summary>
public class BaseModel
{
    /// <summary>
    /// 主键 
    ///</summary>
    [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
    public Guid Id { get; set; }
    /// <summary>
    /// 是否启用 
    ///</summary>
    [SugarColumn(ColumnName = "Enabled", IsNullable = false)]
    public bool? Enabled { get; set; }
    /// <summary>
    /// 排序 
    ///</summary>
    [SugarColumn(ColumnName = "Sort", IsNullable = true)]
    public int? Sort { get; set; }
    
    /// <summary>
    /// 是否删除 
    ///</summary>
    [SugarColumn(ColumnName = "IsDelete", IsNullable = false)]
    public bool IsDelete { get; set; }
    /// <summary>
    /// 最后修改人ID 
    ///</summary>
    [SugarColumn(ColumnName = "LastModifyUserId")]
    public Guid? LastModifyUserId { get; set; }
    /// <summary>
    /// 最后修改时间 
    ///</summary>
    [SugarColumn(ColumnName = "LastModifyTime")]
    public DateTime? LastModifyTime { get; set; }
    /// <summary>
    /// 创建人ID 
    ///</summary>
    [SugarColumn(ColumnName = "CreateUserId")]
    public Guid? CreateUserId { get; set; }
    /// <summary>
    /// 创建时间 
    ///</summary>
    [SugarColumn(ColumnName = "CreateTime")]
    public DateTime? CreateTime { get; set; }
}
