using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySales.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanySales.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public ResultDTO OnPostUpload(IFormFile file, string testid)
        {

            ResultDTO res = new ResultDTO();
            return res;
        }
    }
}