using CoCoSql.Attributer;
using System;

namespace DBModels {
    [Table("T_BalanceLog")]
    public class T_BalanceLog {
        [InsertExclusion]
        public int Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 操作类型, 增加,减少
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 操作金额
        /// </summary>
        public decimal Number { get; set; }
    }
}
