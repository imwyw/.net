using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CompanySales.Repository.Business
{
    /// <summary>
    /// class library 类库中 引用 HttpContext
    /// 参考 https://www.strathweb.com/2016/12/accessing-httpcontext-outside-of-framework-components-in-asp-net-core/
    /// </summary>
    public static class MyHttpContext
    {
        private static IHttpContextAccessor _contextAccessor;

        public static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// 返回token中所有身份信息
        /// 对应 Security 中 GetToken 中写入的身份信息
        /// </summary>
        public static IEnumerable<Claim> Claims => _contextAccessor.HttpContext.User.Claims;

        /// <summary>
        /// 对应 claim 身份信息中的 type 值
        /// 生成 token 时写入的信息
        /// </summary>
        public static string UserName => _contextAccessor.HttpContext.User.FindFirst("UserName").Value;
    }
}
