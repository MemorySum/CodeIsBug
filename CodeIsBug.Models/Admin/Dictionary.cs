namespace CodeIsBug.Models.Admin;

/// <summary>
/// 字典表
///</summary>
[Table(Name ="E_Base_Dictionary")]
public class Dictionary:BaseModel
{
    
    /// <summary>
    /// 字典类型Id 
    ///</summary>
    [Column(Name  = "DictionaryTypeId",IsNullable =true)]
    public Guid DictionaryTypeId { get; set; }
    /// <summary>
    /// 名称 
    ///</summary>
    [Column(Name = "Name", IsNullable = true)]
    public string Name { get; set; }
    /// <summary>
    /// 编码 
    ///</summary>
    [Column(Name =  "Code", IsNullable = true)]
    public string Code { get; set; }
    /// <summary>
    /// 字典值 
    ///</summary>
    [Column(Name =  "Value", IsNullable = true)]
    public string Value { get; set; }
    /// <summary>
    /// 描述 
    ///</summary>
    [Column(Name =  "Description", IsNullable = true)]
    public string Description { get; set; }
 
    /// <summary>
    /// 排序 
    ///</summary>
    [Column(Name  = "Sort", IsNullable = true)]
    public int? Sort { get; set; }

    /// <summary>
    /// 是否删除 
    ///</summary>
    [Column(Name  = "IsDelete", IsNullable = false)]
    public bool IsDelete { get; set; }
    /// <summary>
    /// 最后修改人ID 
    ///</summary>
    [Column(Name  = "LastModifyUserId")]
    public Guid? LastModifyUserId { get; set; }
    /// <summary>
    /// 最后修改时间 
    ///</summary>
    [Column(Name  = "LastModifyTime")]
    public DateTime? LastModifyTime { get; set; }
    /// <summary>
    /// 创建人ID 
    ///</summary>
    [Column(Name  = "CreateUserId")]
    public Guid? CreateUserId { get; set; }
    /// <summary>
    /// 创建时间 
    ///</summary>
    [Column(Name  = "CreateTime")]
    public DateTime? CreateTime { get; set; }

}
