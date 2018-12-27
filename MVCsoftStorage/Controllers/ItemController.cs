using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCsoftStorage.Models;

namespace MVCsoftStorage.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Program(int? id)
        {
            DBContext db = new DBContext();

            int currentId = id ?? 0;
            int postsId = (from el in db.posts
                           where el.id == currentId
                           select el.id).SingleOrDefault();

            if (postsId == 0)
                return View("Error");

            var query =
                from el in db.posts
                where el.id == postsId && el.visible == 1 && el.date_public < DateTime.Now
                select new ProgramModel
                {
                    Name = el.name,
                    Desc = el.description,
                    Spoilers = el.spoilers.ToList(),
                    Imgs = el.images.Where(n => n.type == "screen").ToList(),
                    Program = el.programs,
                    DatePublic = el.date_public ?? DateTime.Now,
                };

            ProgramModel post = query.Single();

            List<ProgramModel> similarPostRequest = 
                                     (from el in post.Program.posts
                                      where el.id != postsId
                                      select new ProgramModel
                                      {
                                          Name = el.name,
                                          Id = el.id
                                      }).Skip(0).Take(11).ToList();

            ItemModel itemModel = new ItemModel
            {
                CurrPost = post,
                SimilarPost = similarPostRequest
            };

            return View(itemModel);
        }

        public FileResult DownloadFile()
        {
            string fullPath = @"~/static/torrent/sims2.torrent";
            return File(fullPath, "application/torrent", "sims.torrent");
        }
    }
}