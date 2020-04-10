using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySales.Model;
using CompanySales.Repository.Business;
using CompanySales.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanySales.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
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
            dto.Data = res;

            return dto;
        }
    }
}