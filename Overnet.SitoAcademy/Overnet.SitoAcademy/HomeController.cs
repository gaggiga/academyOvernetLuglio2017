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
        public string Index()
        {
            var model = new Modello();
            model.Saluto = ConfigurationManager.AppSettings["saluto"];
            model.Data = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            return Vista(model);
        }

        private string Vista(Modello model)
        {
            var result = $"<!DOCTYPE html><html><head><meta charset=\"utf-8\">"
                       + $"<title>Prima prova</title></head>"
                       + $"<body><h2>{model.Saluto}</h2>"
                       + $"<p>{model.Data}</p></body></html>";

            return result;
        }
    }

    public class Modello
    {
        public string Saluto { get; set; }
        public string Data { get; set; }
    }
}