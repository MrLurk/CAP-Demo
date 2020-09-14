using CoCoSql.Attributer;
using System;

namespace DBModels {
    [Table("T_Users")]
    public class T_Users {
        [InsertExclusion]
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
    }
}
