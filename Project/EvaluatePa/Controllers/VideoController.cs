using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EvaluatePa.Models;

namespace EvaluatePa.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult Index(int user)
        {
            return View();
        }
    }
}
