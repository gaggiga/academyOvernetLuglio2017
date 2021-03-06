﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Sql.Entities
{
    [Table("Incassi")]
    public class Incasso
    {
        public int Id { get; set; }
        public DateTime DataIncasso { get; set; }
        public decimal Totale { get; set; }

        public Incasso()
        {
            this.DataIncasso = DateTime.UtcNow;
        }
    }
}
