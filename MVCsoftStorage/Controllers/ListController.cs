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

        public ActionResult Index(int? id, string categ)
        {
            DBContext db = new DBContext();

            if (categ != null)
            {
                var pre_request = from el in db.programs
                                  where el.program_categories.Any(n => n.categories.slug_name == categ)
                                  select el.id;

                var programCat = pre_request.ToList();

                int countPost = (from el in db.posts
                                   join el2 in programCat on el.program_id equals el2
                                   select el).Count();
                Pagintation paginat = new Pagintation(id, countPost);
                paginat.Category = categ;

                var request = (from c in db.posts
                               join c2 in programCat on c.program_id equals c2
                               orderby c.id descending
                               select new ListItem
                               {
                                   Name = c.name,
                                   Id = c.id,
                                   Poster = c.images.Where(image => image.type == "post")
                                                    .Select(image => image.href)
                                                    .FirstOrDefault(),
                               }).Skip(paginat.firstElement).Take(elementPage);

                List<ListItem> content = request.ToList();
                ListContent<ListItem> listContent = new ListContent<ListItem>(content);

                ViewBag.Pagination = paginat;
                ViewBag.Preposts = listContent.GetCardPage(elementLine);
            }
            else
            {
                int countPost = db.posts.Count();
                Pagintation paginat = new Pagintation(id, countPost);
                paginat.Category = "all";

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

                ViewBag.Pagination = paginat;
                ViewBag.Preposts = listContent.GetCardPage(elementLine);
            }
            return View();
        }
    }
}