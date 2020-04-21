using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CompanySales.Repository.Business;
using CompanySales.Repository.Common;
using CompanySales.Repository.Models;
using CompanySales.Repository.Parameter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanySales.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]// 控制器下所有 action 均需要认证
    public class ProductController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 分页查询产品列表信息
        /// Postman 进行测试，需要设置 request body 设置 raw json格式
        /// 默认查询传递 {} 即可， 按名称查询： { "productname":"笔" }
        /// 必须严格 json 规范，否则参数无法对应
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public Pager<Product> GetList([FromBody]ProductParameter parameter)
        {
            var res = ProductBiz.GetListByPage(parameter);
            // 测试获取用户身份信息
            var claims = _httpContextAccessor.HttpContext.User.Claims;

            // 测试写入日志信息
            Log4Helper.Debug("控制器中调用GetList方法，调用者：" + MyHttpContext.UserName);

            return res;
        }


    }
}