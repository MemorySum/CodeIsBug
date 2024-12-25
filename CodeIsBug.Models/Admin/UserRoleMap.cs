using CodeIsBug.Models.Models;
using SqlSugar;

namespace CodeIsBug.Models.Admin
{
    /// <summary>
    /// 用户角色对照表
    ///</summary>
    [SugarTable(TableName =("E_Sys_UserRoleMap"))]
    public class UserRoleMap : BaseModel
    {
        /// <summary>
        /// 1 
        ///</summary>
        [SugarColumn(ColumnName = "UserId")]
        public Guid UserId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "RoleId")]
        public Guid RoleId { get; set; }    
    }
}
