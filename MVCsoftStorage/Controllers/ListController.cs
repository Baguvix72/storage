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
        int elementPage = ViewsSettings.elementPage;
        int elementLine = ViewsSettings.elementLine;

        public ActionResult Index(int? id)
        {
            DBContext db = new DBContext();
            int countPost = db.posts.Count();
            Pagintation paginat = new Pagintation(id, countPost);

            var request = (from c in db.posts
                           orderby c.id descending
                           select new ListItem
                           {
                               Name = c.name,
                               Id = c.id,
                               Poster = c.images.FirstOrDefault(n => n.type == "post").href,
                            }).Skip(paginat.firstElement).Take(elementPage);

            List<ListItem> content = request.ToList();
            ListContent<ListItem> listContent = new ListContent<ListItem>(content);

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