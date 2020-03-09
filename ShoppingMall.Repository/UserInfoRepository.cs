using ShoppingMall.IRepository;
using ShoppingMall.Model;
using ShoppingMall.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMall.Repository
{
    public class UserInfoRepository : BaseRepository<User>, IUserInfoRepository
    {
        public async Task<List<User>> GetUserList()
        {
            return await Select(@"select username,CellPhone as phone,B.GradeName from aspnet_Members A
left join aspnet_Grade B ON A.GradeId=B.Id
where userid in (1,2)");
        }
    }
}
