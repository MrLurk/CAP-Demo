using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoCoSql.Repository;
using DBModels;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api1.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase {

        private readonly ILogger<HomeController> _logger;
        private readonly ICapPublisher _capBus;

        public HomeController(ILogger<HomeController> logger, ICapPublisher capBus) {
            _logger = logger;
            _capBus = capBus;
        }

        [HttpGet]
        public ActionResult Get() {
            return Ok("Api1");
        }

        [HttpGet("TestPublish")]
        public ActionResult TestPublish() {
            var userName = "李四";
            CoCoSqlContext.Insert(new T_Users() {
                UserName = userName,
                Balance = 100M
            });
            var user = CoCoSqlContext.FirstOrDefault<T_Users>(x => x.UserName == userName);
            CoCoSqlContext.Insert(new T_BalanceLog() {
                UserId = user.Id,
                Number = 100M,
                Type = "增加"
            });
            _capBus.Publish("rollback", new { UserId = user.Id });
            return Ok(1);
        }

        [NonAction]
        public void InsertUser() {
           
        }

        [NonAction]
        [CapSubscribe("rollback")]

        private void Rollback(dynamic data) {
            Console.WriteLine("Api1 获取到数据" + data.UserId);
            Console.WriteLine("开始回滚 T_Users 表");
            int userId = Convert.ToInt32(data.UserId);
            CoCoSqlContext.Remove<T_Users>(x => x.Id == userId);
        }
    }
}
