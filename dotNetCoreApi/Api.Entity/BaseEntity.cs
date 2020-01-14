using System;

namespace Api.Entity
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual int? CreateUserID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public virtual int? LastModifyUserID { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime LastModifyDate { get; set; }

        /// <summary>
        /// 实体状态
        /// </summary>
        public virtual EntityState? EntityState { get; set; }
    }

    /// <summary>
    /// 实体状态
    /// </summary>
    public enum EntityState
    {
        /// <summary>
        /// 添加状态
        /// </summary>
        Add = 0,

        /// <summary>
        /// 修改状态
        /// </summary>
        Modify = 1,

        /// <summary>
        /// 删除状态
        /// </summary>
        Delete = 2
    }
}
