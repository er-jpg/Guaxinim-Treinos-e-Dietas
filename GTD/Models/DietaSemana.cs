using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Models
{
    public class DietaSemana
    {
        public int DietaID { get; set; }
        public Dieta Dieta { get; set; }
        public int SemanaID { get; set; }
        public Semana Semana { get; set; }

        // campo extra pra fazer tudo funcionar supostamente
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Dieta")]
        public string DescDieta { get; set; }
    }
}
