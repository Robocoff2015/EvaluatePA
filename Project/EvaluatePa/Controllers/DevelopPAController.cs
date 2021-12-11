using System;
<<<<<<< Updated upstream
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using EvaluatePa.Models;
=======
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvaluatePa.Models;
using System.Data;
using System.Web;
using Microsoft.Data.SqlClient;


>>>>>>> Stashed changes

namespace EvaluatePa.Controllers
{
    public class DevelopPAController : Controller

    {
<<<<<<< Updated upstream
        private string connectionString = @"Data Source = DESKTOP-UK1L50N\SQLEXPRESS;Initial Catalog = EvaluateWork; Integrated Security = True";
        //private string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = EvaluateWork; Integrated Security = True";
        //DESKTOP-UK1L50N\SQLEXPRESS
        //string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Database = EvaluateWork; Integrated Security = True";

        // GET: DevelopPAController

        [HttpGet]
        public ActionResult DevelopPA_Index()
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

                string query = " INSERT INTO DevelopPA(Name,Position,Cdate,Place,BelongTo,GetSalary,RateSalary,TypeClassroom) VALUES (@Name,@Position,@Cdate,@Place,@BelongTo,@GetSalary,@RateSalary,@TypeClassroom)";

                SqlCommand sqlcmd = new SqlCommand(query,sqlcon);

                //sqlcmd.Parameters.AddWithValue("@IdPA",DevelopPAs.IdPA);

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
            return RedirectToAction();
           
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
=======
      // string  connectionString = @"Server=(localdb)\\MSSQLLocalDB;Database=EvaluateWork;Trusted_Connection=True" ;
        string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=EvaluateWork;Trusted_Connection=True";


        
        [HttpGet]

       public IActionResult Index()
        {

        DataTable  dtblDevelopPa = new DataTable();

        using ( SqlConnection    sqlCon = new  SqlConnection(connectionString)) 
        {
           sqlCon.Open();

           SqlDataAdapter sqlDa = new  SqlDataAdapter("SELECT  * FROM  DevelopPA",sqlCon);

            sqlDa.Fill(dtblDevelopPa);

          }

                    return View(dtblDevelopPa);
        }
   

        [HttpGet]
       public IActionResult Create()
        {
            return View(new DevelopPA());
        }

        // Post table  DevelopPA

        [HttpPost]
        public IActionResult Create(DevelopPA developPA)
        {
            using ( SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO DevelopPA VALUES(@IdPA,@Name,@Position,@Place,@Cdate,@BelongTo,@GetSalary,@RateSalary,@TypeClassroom1,@TypeClassroom2,@TypeClassroom3,@TypeClassroom4,@TypeClassroom5)";

                SqlCommand sqlCmd = new SqlCommand(query,sqlCon);

                sqlCmd.Parameters.AddWithValue("@IdPA", developPA.IdPA);
                sqlCmd.Parameters.AddWithValue("@Name", developPA.Name);
                sqlCmd.Parameters.AddWithValue("@Position", developPA.Position);
                sqlCmd.Parameters.AddWithValue("@Place", developPA.Place);

                sqlCmd.Parameters.AddWithValue("@Cdate", developPA.Cdate);
                sqlCmd.Parameters.AddWithValue("@BelongTo", developPA.BelongTo);
                sqlCmd.Parameters.AddWithValue("@GetSalary", developPA.GetSalary);
                sqlCmd.Parameters.AddWithValue("@RateSalary", developPA.RateSalary);

                sqlCmd.Parameters.AddWithValue("@TypeClassroom1", developPA.TypeClassroom1);
                sqlCmd.Parameters.AddWithValue("@TypeClassroom2", developPA.TypeClassroom2);
                sqlCmd.Parameters.AddWithValue("@TypeClassroom3", developPA.TypeClassroom3);
                sqlCmd.Parameters.AddWithValue("@TypeClassroom4", developPA.TypeClassroom4);
                sqlCmd.Parameters.AddWithValue("@TypeClassroom5", developPA.TypeClassroom5);

                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }



 

        /*  [HttpPost]
           [ValidateAntiForgeryToken]                         // post data to attribute table
           public IActionResult Create(DevelopPA context)
           {
               _context.Add(context);
               _context.SaveChanges();
               ViewBag.Message = "This Record  Success";
               return View();
           } */

>>>>>>> Stashed changes
    }
}
