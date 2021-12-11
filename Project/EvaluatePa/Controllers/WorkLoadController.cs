using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using EvaluatePa.Models;

namespace EvaluatePa.Controllers
{
    public class WorkLoadController : Controller
    {
        // GET: WorkLoadController
        [HttpGet]
        public ActionResult WorkLoad_Index()
        {
            return View();
        }


     /*   [HttpPost]
        public JsonResult SaveWork(WorkLoadVM w)

        {
            bool status = false;
            if(ModelState.IsValid)
            {
                using (MyDatabaseEntities  dc = new MyDatabaseEntities())
                {
                    WorkLoadPart1 workloadpart1 = new WorkLoadPart1 { AmountWorkLoad = w.AountWorkLoad};
                    WorkLoadPart3 workloadpart3 = new WorkLoadPart3 { HourSupportSubject = w.HourSupportSubject, HourQsubject=w.HourQsubject , HourFocus =w.HourFocus};
                     foreach (var i in w.WorkLoadPart2)

                            {
                               
                    }
                }*/
            } 
        } 