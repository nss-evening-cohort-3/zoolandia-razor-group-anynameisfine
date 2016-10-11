using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoolandiaRazor.DAL;

namespace ZoolandiaRazor.Controllers
{
    public class HabitatController : Controller
    {
        ZooRepository repo = new ZooRepository();
        // GET: Habitat
        public ActionResult Index()
        {
            ViewBag.habitats = repo.GetHabitats();
            return View();
        }

        // GET: Habitat/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.habitat = repo.FindHabitat(id);
            return View();
        }

    }
}
