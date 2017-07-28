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

            model.Studenti.Add("Giovanni Improta");
            model.Studenti.Add("Mauro Sanna");
            model.Studenti.Add("Marcantonio Cilia");
            model.Studenti.Add("Alexandru Polevoi");
            model.Studenti.Add("Florio Pasquale");
            model.Studenti.Add("Nino La Corte");
            model.Studenti.Add("Antonio Papa");
            model.Studenti.Add("Gianluca Dell'Atti");

            return View(model);
        }
    }

    public class Modello
    {
        public string Saluto { get; set; }
        public string Data { get; set; }
        public SortedSet<string> Studenti { get; set; }

        public Modello()
        {
            this.Studenti = new SortedSet<string>();
        }
    }
}