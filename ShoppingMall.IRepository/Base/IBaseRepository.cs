using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMall.IRepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> Select(string sql, object param = null);

        Task<TEntity> Detail(string sql, object param = null);

        Task ExecuteNonQuery(TEntity entity, string sql);

        Task ExecuteNonQuery(List<TEntity> entityList, string sql);
    }
}
