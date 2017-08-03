using GadgetStore.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GadgetStore.WebApi.Amministrazione.Catalogo
{
    [OnlyOne("FamigliaId", "PadreId", ErrorMessage = "Indicare un id famiglia o (XOR) un id padre.")]
    public class CategoriePost
    {
        [Required, StringLength(255)]
        public string Nome { get; set; }
        public int? FamigliaId { get; set; }
        public int? PadreId { get; set; }
    }
}