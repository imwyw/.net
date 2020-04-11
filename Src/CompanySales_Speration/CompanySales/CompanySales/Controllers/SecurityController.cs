using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CompanySales.Model;
using CompanySales.Repository.Business;
using CompanySales.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CompanySales.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public SecurityController(IConfiguration configuration)
        {
            // 注入configuration 配置，可以访问配置信息
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        [HttpGet]
        public ResultDTO Login(string uid, string pwd)
        {
            var dto = new ResultDTO();

            if (null == uid || null == pwd)
            {
                return dto;
            }
            var res = UserBiz.Login(uid, pwd);
            dto.Status = res != null;
            string jwt = dto.Status ? GetToken(res) : string.Empty;

            // 构造匿名对象返回，包含token信息
            dto.Data = new { User = res, Token = jwt };

            return dto;
        }

        private string GetToken(User user)
        {
            // 此处秘钥需要和 Startup 中保持一致
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["token:key"]));

            SecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["token:issuer"],
                audience: Configuration["token:audience"],
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.Now.AddMinutes(20),
                claims: new Claim[] {
                    // 增加身份信息，此处设置角色为 Admin，与标签 [Authorize(Roles = "Admin")] 匹配
                    new Claim(ClaimTypes.Role,user.Roles)
                }
            );

            string jwt = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return jwt;
        }

    }
}