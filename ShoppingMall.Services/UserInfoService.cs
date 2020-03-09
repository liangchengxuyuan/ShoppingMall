using ShoppingMall.IRepository;
using ShoppingMall.IServices;
using ShoppingMall.Model;
using ShoppingMall.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMall.Services
{
    public class UserInfoService : BaseServices<User>, IUserInfoService
    {
        IUserInfoRepository _dal;

        public UserInfoService(IUserInfoRepository dal)
        {
            base._baseDal = dal;
            this._dal = dal;
        }

        public async Task<List<User>> GetUserList()
        {
            return await _dal.GetUserList();
        }
    }
}
