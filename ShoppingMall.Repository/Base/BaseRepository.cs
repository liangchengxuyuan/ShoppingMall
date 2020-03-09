using Dapper;
using ShoppingMall.IRepository.Base;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingMall.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">执行的sql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Select(string sql, object param = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                if (null == param)
                {
                    return await Task.Run(() => conn.Query<TEntity>(sql).ToList());
                }

                return await Task.Run(() => conn.Query<TEntity>(sql, param).ToList());
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sql">执行的sql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<TEntity> Detail(string sql, object param = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                if (null == param)
                {
                    return await Task.Run(() => conn.Query<TEntity>(sql).FirstOrDefault());
                }
                return await Task.Run(() => conn.Query<TEntity>(sql, param).FirstOrDefault());
            }
        }

        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="entity">源数据</param>
        /// <param name="sql">执行的sql</param>
        /// <returns></returns>
        public async Task ExecuteNonQuery(TEntity entity, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                await conn.ExecuteAsync(sql, entity);
            }
        }

        /// <summary>
        /// 批量执行增删改操作
        /// </summary>
        /// <param name="entityList">源数据</param>
        /// <param name="sql">执行的sql</param>
        /// <returns></returns>
        public async Task ExecuteNonQuery(List<TEntity> entityList, string sql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection())
            {
                await conn.ExecuteAsync(sql, entityList);
            }
        }
    }
}
