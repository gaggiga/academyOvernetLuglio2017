using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Overnet.SitoAcademy
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var model = new Modello();
            model.Saluto = ConfigurationManager.AppSettings["saluto"];
            model.Data = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            return View(model);
        }
    }

    public class Modello
    {
        public string Saluto { get; set; }
        public string Data { get; set; }
    }
}