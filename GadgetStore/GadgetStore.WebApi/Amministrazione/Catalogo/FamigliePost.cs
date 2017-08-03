using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GadgetStore.WebApi.Amministrazione.Catalogo
{
    public class FamigliePost
    {
        [Required, StringLength(255)]
        public string Nome { get; set; }
    }
}