using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoolandiaRazor.DAL;
using ZoolandiaRazor.Models;

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

        public ActionResult Add()
        {
            ViewBag.Habitat = repo.GetHabitats();
            return View();
        }

        [HttpPost]
        public ActionResult Add(Employee emp)
        {
            if (ModelState.IsValid)
            {
                repo.AddEmployee(emp);
                return RedirectToAction("Index");
            }

            return View(emp);
        }
    }
}
