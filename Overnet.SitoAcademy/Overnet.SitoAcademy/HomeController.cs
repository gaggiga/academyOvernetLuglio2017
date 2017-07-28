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
            var saluto = ConfigurationManager.AppSettings["saluto"];
            var data = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            return Vista(saluto, data);
        }

        private string Vista(string saluto, string data)
        {
            var result = $"<!DOCTYPE html><html><head><meta charset=\"utf-8\">"
                       + $"<title>Prima prova</title></head>"
                       + $"<body><h2>{saluto}</h2>"
                       + $"<p>{data}</p></body></html>";

            return result;
        }
    }
}