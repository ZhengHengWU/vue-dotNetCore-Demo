using Dapper;

namespace Api.Entity.Common
{
    /// <summary>
    /// 序列表
    /// </summary>
    [Table("Sequence")]
    public class SequenceEntity : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 日期编号
        /// </summary>
        public string DateNumber { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Number { get; set; }

    }
}
