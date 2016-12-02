using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Madera.Service;

namespace MaderaSoft.Areas.RessourcesHumaines.Controllers
{
    public class EmployeController : Controller
    {
        //private readonly EmployeService employeService;

        // GET: RessourcesHumaines/Employe
        public ActionResult Index()
        {
            return View();
        }
    }
}