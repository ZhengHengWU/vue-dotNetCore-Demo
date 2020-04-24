using Api.Entity.Logs;
using Api.Repository;
using System;
using System.Collections.Generic;

namespace Api.Service
{
    public class ErrorService
    {
        /// <summary>
        /// 保存一条错误信息
        /// </summary>
        /// <param name="error"></param>
        public void AddError(ErrorEntity error)
        {
            try
            {
                BaseRepository<ErrorEntity>.Instance.Insert(error);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 保存多条错误信息
        /// </summary>
        /// <param name="errors"></param>
        public void AddError(IEnumerable<ErrorEntity> errors)
        {
            try
            {
                BaseRepository<ErrorEntity>.Instance.Insert(errors);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
