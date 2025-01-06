namespace CodeIsBug.Models.Admin;

/// <summary>
/// 省市信息表
///</summary>
[Table(Name ="E_Base_City")]
public class EBaseCity:BaseModel
{
    
    /// <summary>
    ///  上级ID
    ///</summary>
    [Column(Name  = "PID")]
    public Guid? PID { get; set; }
    /// <summary>
    ///  
    ///</summary>
    [Column(Name  = "Level")]
    public int? Level { get; set; }
    /// <summary>
    ///  
    ///</summary>
    [Column(Name  = "Name")]
    public string Name { get; set; }
    /// <summary>
    ///  
    ///</summary>
    [Column(Name  = "Pinyin_Prefix")]
    public string PinyinPrefix { get; set; }
    /// <summary>
    ///  
    ///</summary>
    [Column(Name  = "Pinyin")]
    public string Pinyin { get; set; }
    /// <summary>
    ///  
    ///</summary>
    [Column(Name  = "FullId")]
    public Guid? FullId { get; set; }
    /// <summary>
    ///  
    ///</summary>
    [Column(Name  = "FullName")]
    public string FullName { get; set; }
}
