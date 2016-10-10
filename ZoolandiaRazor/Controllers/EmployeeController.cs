using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoolandiaRazor.DAL;

namespace ZoolandiaRazor.Controllers
{
    public class EmployeeController : Controller
    {
        ZooRepository repo = new ZooRepository();
        // GET: Employee
        public ActionResult Index()
        {
            ViewBag.employees = repo.GetEmployees();
            return View();
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Employee = repo.FindEmployee(id);
            return View();
        }
    }
}
