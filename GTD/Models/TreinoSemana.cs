using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Models
{
    public class TreinoSemana
    {
        public int? TreinoID { get; set; }
        public Treino Treino { get; set; }
        public int? SemanaID { get; set; }
        public Semana Semana { get; set; }

        // campo extra pra fazer tudo funcionar supostamente
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Treino")]
        public string DescTreino { get; set; }
    }
}
