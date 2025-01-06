namespace CodeIsBug.Models.Admin;

/// <summary>
/// 字典类型表
///</summary>
[Table(Name ="E_Base_DictionaryType")]
public class DictionaryType:BaseModel
{
    /// <summary>
    /// 名称 
    ///</summary>
   [Column(Name = "Name")]
    public string Name { get; set; }
    /// <summary>
    /// 编码 
    ///</summary>
   [Column(Name = "Code")]
    public string Code { get; set; }
    /// <summary>
    /// 描述 
    ///</summary>
   [Column(Name = "Description")]
    public string Description { get; set; }
    /// <summary>
    /// 是否启用 
    ///</summary>
   [Column(Name = "Enabled")]
    public bool Enabled { get; set; }
    /// <summary>
    /// 排序 
    ///</summary>
   [Column(Name = "Sort")]
    public int? Sort { get; set; }
}
