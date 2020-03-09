using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingMall.Common.Authorization;
using ShoppingMall.IServices;
using ShoppingMall.Model;

namespace ShoppingMall.Controllers
{
    /// <summary>
    /// 测试模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy= "Client")]
    public class ValuesController : ControllerBase
    {
        private IUserInfoService _userInfoService;

        public ValuesController(IUserInfoService userInfoService)
        {
            this._userInfoService = userInfoService;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var userList = await _userInfoService.GetUserList();

            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost("GetToken")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody] JwtToken jwtToken)
        {
            var jwt = jwtToken;
            var token = JwtHelper.IssueJWT(jwt, TimeSpan.FromMinutes(30));
            return Ok(new
            {
                token = token
            });
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="id">测试id</param>
        /// <returns>用户列表</returns>
        [HttpGet("GetUser")]
        public async Task<ActionResult<List<User>>> GetUser(string id)
        {
            var userList = await _userInfoService.GetUserList();

            return userList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] User token)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
