using ShoppingMall.IRepository.Base;
using ShoppingMall.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMall.IRepository
{
    public interface IUserInfoRepository : IBaseRepository<User>
    {
        Task<List<User>> GetUserList();
    }
}
