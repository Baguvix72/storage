using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCsoftStorage.Class;

namespace MVCsoftStorage.Controllers
{
    public class ListController : Controller
    {
        DBContext db = new DBContext();
        int elementPage = ViewsSettings.elementPage;
        int elementLine = ViewsSettings.elementLine;

        public ActionResult Index(int? id)
        {
            Pagintation paginat = new Pagintation(id);

            var request = (from c in db.preposts
                            orderby c.id descending
                            select c).Skip(paginat.firstElement).Take(elementPage);

            List<preposts> content = request.ToList();
            ListContent listContent = new ListContent(content);

            ViewBag.Preposts = listContent.GetCardPage(elementLine);
            ViewBag.Pagination = paginat;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}