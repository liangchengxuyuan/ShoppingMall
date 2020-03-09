using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMall.IServices.Base
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        Task<List<TEntity>> Select(string sql, object param = null);

        Task<TEntity> Detail(string sql, object param = null);

        Task ExecuteNonQuery(TEntity entity, string sql);

        Task ExecuteNonQuery(List<TEntity> entityList, string sql);
    }
}
