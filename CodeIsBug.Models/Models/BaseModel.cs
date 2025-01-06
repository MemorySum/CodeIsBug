
namespace CodeIsBug.Models.Models;

/// <summary>
/// 实体通用属性
/// </summary>
public class BaseModel
{
    /// <summary>
    /// 主键 
    ///</summary>
    [Column(Name = "Id", IsPrimary = true)]
    public Guid Id { get; set; }
   
}
