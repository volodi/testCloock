using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cloocks.Server.Entities;

namespace Cloocks.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly MongoRepository<Clock> clocks = new MongoRepository<Clock>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }



        [HttpGet]
        [Route("addClock")]
        public async Task<IActionResult> AddClock()
        {
            Clock db = new Clock();
            var clcok = new Clock()
            {
                Name = "3",
                Price = 10,
                Data = "some desc"
            };
            var clcok1 = new Clock()
            {
                Name = "2",
                Price = 20,
                Data = "some desc2"
            };
            var res = await clocks.InsertOneAsync(clcok);
            //var res1 = await clocks.GetAll();
            var res2 = await clocks.InsertOneAsync(clcok1);
            //var res3 = await clocks.GetAll();
            //ViewBag.Clock = res3;


            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


    }
}
