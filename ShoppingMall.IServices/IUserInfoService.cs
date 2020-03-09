using ShoppingMall.IServices.Base;
using ShoppingMall.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMall.IServices
{
    public interface IUserInfoService : IBaseServices<User>
    {
        Task<List<User>> GetUserList();
    }
}
