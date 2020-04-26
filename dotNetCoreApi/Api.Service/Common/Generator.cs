using Api.Repository;

namespace Api.Service.Common
{
    public class Generator
    {
        /// <summary>
        /// 创建异常代码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string CreateErrorCode(int userId)
        {
            return SequenceRepository.CreateSequenceNumber(userId);
        }


    }
}
