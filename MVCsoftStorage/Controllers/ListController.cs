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

        [HttpGet]
        public ActionResult Index(int? id, string categ)
        {
            DBContext db = new DBContext();
            Pagintation mainPagination = new Pagintation(id, 0, categ);

            if (categ != null && categ != "all")
            {
                var pre_request = from el in db.programs
                                  where el.program_categories.Any(n => n.categories.slug_name == categ)
                                  select el.id;

                var programCat = pre_request.ToList();

                int countPost = (from el in db.posts
                                   join el2 in programCat on el.program_id equals el2
                                   select el).Count();
                mainPagination = new Pagintation(id, countPost, categ);

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
                               }).Skip(mainPagination.firstElement).Take(elementPage);

                List<ListItem> content = request.ToList();
                ListContent<ListItem> listContent = new ListContent<ListItem>(content);

                ViewBag.Preposts = listContent.GetCardPage(elementLine);
            }
            else
            {
                int countPost = db.posts.Count();
                mainPagination = new Pagintation(id, countPost, "all");

                var request = (from c in db.posts
                               orderby c.id descending
                               select new ListItem
                               {
                                   Name = c.name,
                                   Id = c.id,
                                   Poster = c.images.FirstOrDefault(n => n.type == "post").href,
                               }).Skip(mainPagination.firstElement).Take(elementPage);

                List<ListItem> content = request.ToList();
                ListContent<ListItem> listContent = new ListContent<ListItem>(content);

                ViewBag.Preposts = listContent.GetCardPage(elementLine);
            }

            ViewBag.Pagination = mainPagination;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? id, string categ, string search)
        {
            DBContext db = new DBContext();

            var pre_request = from el in db.programs
                              where el.name.Contains(search)
                              select el.name;

            var programFound = pre_request.ToList();

            Pagintation mainPagination = new Pagintation(id, 0, categ);

            if (programFound.Count() > 0)
            {
                int countPost = (from el in db.posts
                                 join el2 in programFound on el.programs.name equals el2
                                 select el).Count();
                mainPagination = new Pagintation(id, countPost, categ);
            }

            var request = (from c in db.posts
                           join c2 in programFound on c.programs.name equals c2
                           orderby c.id descending
                           select new ListItem
                           {
                               Name = c.name,
                               Id = c.id,
                               Poster = c.images.Where(image => image.type == "post")
                                                .Select(image => image.href)
                                                .FirstOrDefault(),
                           }).Skip(mainPagination.firstElement).Take(elementPage);

            List<ListItem> content = request.ToList();
            ListContent<ListItem> listContent = new ListContent<ListItem>(content);

            ViewBag.Preposts = listContent.GetCardPage(elementLine);

            ViewBag.Pagination = mainPagination;
            return View();
        }
    }
}