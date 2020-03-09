using ShoppingMall.IRepository.Base;
using ShoppingMall.IServices.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMall.Services.Base
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        public IBaseRepository<TEntity> _baseDal;

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">执行的sql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<List<TEntity>> Select(string sql, object param = null)
        {
            return _baseDal.Select(sql, param);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sql">执行的sql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<TEntity> Detail(string sql, object param = null)
        {
            return _baseDal.Detail(sql, param);
        }

        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="entity">源数据</param>
        /// <param name="sql">执行的sql</param>
        /// <returns></returns>
        public Task ExecuteNonQuery(TEntity entity, string sql)
        {
            return _baseDal.ExecuteNonQuery(entity, sql);
        }

        /// <summary>
        /// 批量执行增删改操作
        /// </summary>
        /// <param name="entityList">源数据</param>
        /// <param name="sql">执行的sql</param>
        /// <returns></returns>
        public Task ExecuteNonQuery(List<TEntity> entityList, string sql)
        {
            return _baseDal.ExecuteNonQuery(entityList, sql);
        } 
    }
}
