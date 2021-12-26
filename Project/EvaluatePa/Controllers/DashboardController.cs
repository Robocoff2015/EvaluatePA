using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EvaluatePa.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard_Index()
        {
            return View();
        }
        public IActionResult Dashboard_Info()
        {
            return View();
        }
        public IActionResult Dashboard_Evaluation()
        {
            return View();
        }
        public IActionResult Dashboard_Result()
        {
            return View();
        }
    }
}
