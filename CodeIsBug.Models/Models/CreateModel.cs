using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace CodeIsBug.Models.Models
{
    public class CreateModel:BaseModel
    {/// <summary>
     /// 创建人guid 
     ///</summary>
        [SugarColumn(ColumnName = "CreateUserId", IsNullable = false)]
        public Guid CreateUserId { get; set; }
        /// <summary>
        /// 创建时间 
        ///</summary>
        [SugarColumn(ColumnName = "CreateTime", IsNullable = false)]
        public DateTime CreateTime { get; set; }
    }
}
