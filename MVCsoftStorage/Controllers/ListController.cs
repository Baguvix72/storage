using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCsoftStorage.CustomClass;
using MVCsoftStorage.Models;

namespace MVCsoftStorage.Controllers
{
    public class ListController : Controller
    {
        public ActionResult All(int? id)
        {
            DBContext db = new DBContext();

            int countPost = db.posts.Count();
            PagintationModel Pagination = new PagintationModel(id, countPost);

            var request = (from c in db.posts
                           orderby c.id descending
                           select new ProgramModel
                           {
                               Name = c.name,
                               Id = c.id,
                               Poster = c.images.FirstOrDefault(n => n.type == "post").href,
                           }).Skip(Pagination.FirstElement).Take(ViewsSettings.elementPage);

            ListProgramModel content = new ListProgramModel(request.ToList());
            ViewBag.Pagination = Pagination;

            return View(content);
        }

        public ActionResult Filter(string categ, int? id)
        {
            DBContext db = new DBContext();

            var requestCat = from el in db.categories
                             select el.slug_name;
            var CatList = requestCat.ToList();

            if (!CatList.Contains(categ))
                return RedirectToAction("All");

            var requestCatProgram = from el in db.programs
                                    where el.program_categories.Any(n => n.categories.slug_name == categ)
                                    select el.id;
            var programCat = requestCatProgram.ToList();

            int countPost = (from el in db.posts
                             join el2 in programCat on el.program_id equals el2
                             select el).Count();

            PagintationModel Pagination = new PagintationModel(id, countPost);
            Pagination.Controller = "ListFilter";
            Pagination.Category = categ;

            var request = (from c in db.posts
                           join c2 in programCat on c.program_id equals c2
                           orderby c.id descending
                           select new ProgramModel
                           {
                               Name = c.name,
                               Id = c.id,
                               Poster = c.images.Where(image => image.type == "post")
                                                .Select(image => image.href)
                                                .FirstOrDefault(),
                           }).Skip(Pagination.FirstElement).Take(ViewsSettings.elementPage);

            ListProgramModel content = new ListProgramModel(request.ToList());

            ViewBag.Pagination = Pagination;
            return View("All", content);
        }

        public ActionResult Search(int? id, string search)
        {
            DBContext db = new DBContext();

            var pre_request = from el in db.programs
                              where el.name.Contains(search)
                              select el.name;

            var programFound = pre_request.ToList();

            int countPost = (from el in db.posts
                             join el2 in programFound on el.programs.name equals el2
                             select el).Count();

            PagintationModel Pagination = new PagintationModel(id, countPost);

            var request = (from c in db.posts
                           join c2 in programFound on c.programs.name equals c2
                           orderby c.id descending
                           select new ProgramModel
                           {
                               Name = c.name,
                               Id = c.id,
                               Poster = c.images.Where(image => image.type == "post")
                                                .Select(image => image.href)
                                                .FirstOrDefault(),
                           }).Skip(Pagination.FirstElement).Take(ViewsSettings.elementPage);

            ListProgramModel content = new ListProgramModel(request.ToList());
            ViewBag.Pagination = Pagination;

            return View("All", content);
        }
    }
}