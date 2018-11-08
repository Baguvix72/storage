using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCsoftStorage.Class;

namespace MVCsoftStorage.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Index(int? id)
        {
            DBContext db = new DBContext();
            int idPost = id ?? -1;

            if (idPost != -1)
            {
                var query =
                    from el in db.posts
                    where el.id == idPost && el.visible == 1 && el.date_public < DateTime.Now
                    select new ItemContent
                    {
                        Name = el.name,
                        Desc = el.description,
                        Spoilers = el.spoilers.ToList(),
                        Poster = el.images.FirstOrDefault(n => n.type == "post").href,
                    };

                ViewBag.Post = query.SingleOrDefault();
            }
            else
            {
                posts posts = db.posts.OrderByDescending(n => n.id).First();
                ItemContent itemContent = new ItemContent
                {
                    Name = posts.name,
                    Desc = posts.description,
                    Spoilers = posts.spoilers.ToList(),
                    Poster = posts.images.FirstOrDefault(n => n.type == "post").href,
                };

                ViewBag.Post = itemContent;
            }
            return View();
        }
    }
}