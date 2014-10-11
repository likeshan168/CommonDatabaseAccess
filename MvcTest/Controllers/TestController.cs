using MvcTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTest.Controllers
{
    public class TestController : Controller
    {
        [Ninject.Inject]
        private ILogic logic { get; set; }
        public ActionResult Index()
        {
            return View(logic.GetPersonInfos());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Person person)
        {
            logic.AddPersonInfo(person);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View(logic.GetPersonInfo(id));
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            logic.EditPersonInfo(person);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(logic.GetPersonInfo(id));
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(logic.GetPersonInfo(id));
        }
        [HttpPost]
        public ActionResult Delete(Person person)
        {
            logic.DeletePersonInfo(person);
            return RedirectToAction("Index");
        }


        public JsonResult GetMobile()
        {
            
            return Json(logic.GetMobile(),JsonRequestBehavior.AllowGet);
        }
    }
}