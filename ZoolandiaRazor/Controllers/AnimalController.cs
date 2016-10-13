using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoolandiaRazor.DAL;

namespace ZoolandiaRazor.Controllers
{
    public class AnimalController : Controller
    {
        ZooRepository repo = new ZooRepository();
        // GET: Animal
        public ActionResult Index()
        {
            ViewBag.animals = repo.GetAnimals();
            return View();
        }

        // GET: Animal/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Animal = repo.FindAnimal(id);
            return View();
        }

    }
}
