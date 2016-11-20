using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Madera.Model;
using Madera.Service;
using MaderaSoft.Models;
using MaderaSoft.Models.Bootstrap;

namespace MaderaSoft.Controllers
{
    public class TracabiliteController : Controller
    {
        private readonly IApplicationTraceService appTraceService;

        public TracabiliteController(IApplicationTraceService applicationTraceService)
        {
            this.appTraceService = applicationTraceService;
        }

        // GET: Tracabilite
        public ActionResult Index()
        {
            BootstrapTableModel modelOut = new BootstrapTableModel();
            List<ApplicationTraceDTO> lesTraces = Mapper.Map<List<ApplicationTrace>, List<ApplicationTraceDTO>>(appTraceService.getAll().ToList());

            modelOut.avecActionCrud = false;
            modelOut.lesLignes.Add(new List<string> { "Utilisateur", "Date", "Action", "Description"});
            

            foreach (ApplicationTraceDTO appTraceDTO in lesTraces)
            {
                modelOut.lesLignes.Add(new List<string> { appTraceDTO.utilisateur, appTraceDTO.date.ToString(), appTraceDTO.action, appTraceDTO.description});
            }


            return View(modelOut);
        }
    }
}