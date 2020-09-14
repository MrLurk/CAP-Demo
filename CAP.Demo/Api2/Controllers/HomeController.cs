using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoCoSql.Repository;
using DBModels;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api2.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get() {
            return Ok("Api2");
        }

        [NonAction]
        [CapSubscribe("rollback")]

        private void Rollback(dynamic data) {
            Console.WriteLine("Api2 获取到数据" + data.UserId);
            Console.WriteLine("开始回滚 T_BalanceLog 表");
            int userId = Convert.ToInt32(data.UserId);
            CoCoSqlContext.Remove<T_BalanceLog>(x=>x.UserId == userId);
        }
    }
}
