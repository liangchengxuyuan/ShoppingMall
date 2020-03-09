namespace ShoppingMall.Model
{
    /// <summary>
    /// JWT令牌类
    /// </summary>
    public class JwtToken
    {
        public JwtToken()
        {
            this.UserId = 0;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 身份
        /// </summary>
        public string Sub { get; set; }
    }
}
