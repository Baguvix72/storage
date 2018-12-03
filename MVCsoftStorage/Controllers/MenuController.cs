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
        public ActionResult Index(string categ)
        {
            DBContext db = new DBContext();

            var query =
                from el in db.categories
                select el;

            if (categ == null)
                categ = "all";

            ViewBag.Active = categ;
            ViewBag.Categories = query.ToList();

            return PartialView("_Menu");
        }
    }
}