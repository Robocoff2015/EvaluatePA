using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using EvaluatePa.Models;

namespace EvaluatePa.Controllers
{
    public class DevelopPAController : Controller

    {
        private string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = EvaluateWork; Integrated Security = True";
     
        //string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Database = EvaluateWork; Integrated Security = True";

        // GET: DevelopPAController
        
        [HttpGet]
        public ActionResult Index()
        {

            DataTable dtblDevelopPA = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
              {

                sqlcon.Open();
               
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT *  FROM   DevelopPA", sqlcon);
                
                sqlda.Fill(dtblDevelopPA);
              
            } 
            return View(dtblDevelopPA);
            
        }


        // GET: DevelopPAController/Details/5
      /*  public ActionResult Details(int id)
        {
            return View();
        }  */

        // GET: DevelopPAController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new DevelopPAModels()); 
        }

        // POST: DevelopPAController/Create
        // [ValidateAntiForgeryToken]

       [HttpPost]
        public ActionResult Create(DevelopPAModels DevelopPAs)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {

                sqlcon.Open();

                string query = " INSERT INTRO DevelopPA(IdPA,Name,Position,Cdate,Place,BelongTo,GetSalaty,RateSalary,TypeClassroom) VALUES (@IdPA,@Name,@Position,@Cdate,@Place,@BelongTo,@GetSalaty,@RateSalary,@TypeClassroom)";

                SqlCommand sqlcmd = new SqlCommand(query,sqlcon);

                sqlcmd.Parameters.AddWithValue("@IdPA",DevelopPAs.IdPA);

                sqlcmd.Parameters.AddWithValue("@Name", DevelopPAs.Name);

                sqlcmd.Parameters.AddWithValue("@Position", DevelopPAs.Position);

                sqlcmd.Parameters.AddWithValue("@Cdate", DevelopPAs.Cdate);

                sqlcmd.Parameters.AddWithValue("@Place", DevelopPAs.Place);

                sqlcmd.Parameters.AddWithValue("@BelongTo", DevelopPAs.BelongTo);

                sqlcmd.Parameters.AddWithValue("@GetSalary", DevelopPAs.GetSalary);

                sqlcmd.Parameters.AddWithValue("@RateSalary", DevelopPAs.RateSalary);

                sqlcmd.Parameters.AddWithValue("@TypeClassroom", DevelopPAs.TypeClassroom);

                sqlcmd.ExecuteNonQuery();
               
                //sqlcon.Close();
                //Console.WriteLine("Success Create");

            }
            return RedirectToAction("Index");
           
        }

        // GET: DevelopPAController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DevelopPAController/Edit/5
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

        // GET: DevelopPAController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DevelopPAController/Delete/5
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
