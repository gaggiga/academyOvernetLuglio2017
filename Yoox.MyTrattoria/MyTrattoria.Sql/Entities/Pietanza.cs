using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrattoria.Sql.Entities
{
    [Table("Pietanze")]
    public class Pietanza
    {
        public int Id { get; set; }
        [Required, StringLength(150)]
        public string Nome { get; set; }
        [Required, StringLength(50)]
        public string Tipo { get; set; }
        public decimal Prezzo { get; set; }
    }
}
