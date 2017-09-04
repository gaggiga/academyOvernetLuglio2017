using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.CampoCalcolato
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creo la riga");

            var riga = new RigaOrdine()
            {
                Descrizione = "Patate fritte",
                Prezzo = 2.50m,
                Quantità = 10,
                Sconto = 2.50m
            };

            Console.WriteLine("Riga creata");
            Console.WriteLine("Salvo la riga");

            using (var db = new MyDbContext())
            {
                db.RigheOrdine.Add(riga);
                db.SaveChanges();
            }
            Console.WriteLine("Riga salvata");
            Console.WriteLine("Leggo la riga");

            using (var db = new MyDbContext())
            {
                var laRiga = db.RigheOrdine.OrderByDescending(r => r.Id).First();

                Console.WriteLine($"Acquistate {laRiga.Quantità} {laRiga.Descrizione} per un totale di {laRiga.TotaleRiga}");
            }

            Console.WriteLine("Riga letta");

            Console.WriteLine("------------------------------------------------");

            Console.WriteLine("Creo l'ordine");
            var ordine = new Ordine();
            
            using(var db = new MyDbContext())
            {
                db.RigheOrdine.ToList().ForEach(r => ordine.RigheOrdine.Add(r));
            }

            Console.WriteLine("Ordine creato");
            Console.WriteLine("Salvo l'ordine");

            using (var db = new MyDbContext())
            {
                db.Ordini.Add(ordine);
                db.SaveChanges();
            }

            Console.WriteLine("Ordine salvato");
            Console.WriteLine("Leggo l'ordine");

            using (var db = new MyDbContext())
            {
                var lOrdine = db.Ordini.OrderByDescending(r => r.Id).First();

                Console.WriteLine($"Ordine del {lOrdine.Data:HH:mm.ss dd:MM:yyyy} per un totale di € {lOrdine.TotaleOrdine}");
            }

            Console.WriteLine("Ordine letto");

            Console.ReadKey();
        }
    }
}
