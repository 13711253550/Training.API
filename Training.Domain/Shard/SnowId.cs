using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowflake.Core;

namespace Training.Domain.Shard
{
    public class SnowId
    {
       /// <summary>
       /// 生成学花ID
       /// </summary>
       /// <returns></returns>
        public static string GetId()
        {
            //1.创建对象
            IdWorker idWorker = new IdWorker(1, 1);
            //2.获取ID
            long id = idWorker.NextId();
            return id.ToString();
        }
    }
}
