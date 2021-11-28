using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EvaluatePa.Controllers
{
    public class EvaluatePAController : Controller
    {
        public IActionResult EvaluatePA_Index()
        {
            return View();
        }
        public IActionResult PA_Form()
        {
            return View();
        }
    }
}
