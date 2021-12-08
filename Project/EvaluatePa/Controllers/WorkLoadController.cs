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





        // GET: WorkLoadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkLoadController/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: WorkLoadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkLoadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkLoadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkLoadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkLoadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
