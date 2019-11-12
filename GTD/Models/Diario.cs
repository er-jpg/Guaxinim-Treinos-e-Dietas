using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Models
{
    public class Diario
    {
        public int? DiarioID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DataDiario { get; set; } = DateTime.Now;

        public bool CompletoTreino { get; set; } = false;
        public bool CompletoDieta { get; set; } = false;

        // FK
        public int? PlanoID { get; set; }
        public virtual Plano Plano { get; set; }
    }
}
