using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCsoftStorage.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            DBContext db = new DBContext();

            var query =
                from el in db.categories
                select el;

            ViewBag.Categories = query.ToList();

            return PartialView("_Menu");
        }
    }
}