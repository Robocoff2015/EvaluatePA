using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EvaluatePa.Controllers
{
    public class VideoController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
