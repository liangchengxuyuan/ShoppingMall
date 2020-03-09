using ShoppingMall.Common.Config;
using StackExchange.Redis;

namespace ShoppingMall.Common.Cache
{
    public class RedisManager
    {
        private static ConnectionMultiplexer instance;


        private static string conn = ConfigHelper.GetSectionValue("connRedis");
        private static readonly object locker = new object();

        private RedisManager()
        {

        }
        /// <summary>
        /// 单例获取连接
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = ConnectionMultiplexer.Connect(conn);
                    }
                }
                return instance;
            }
        }
    }
}
