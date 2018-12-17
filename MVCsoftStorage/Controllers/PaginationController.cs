using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCsoftStorage.Models;

namespace MVCsoftStorage.Controllers
{
    public class PaginationController : Controller
    {
        // GET: Pagination
        public ActionResult Index(PagintationModel pagintaion)
        {
            return PartialView("_Pagination", pagintaion);
        }
    }
}