using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.ViewModels
{
    public class DietaSemanaViewModel
    {
        public int SemanaID { get; set; }
        public SelectList Semanas { get; set; }
        public string Texto { get; set; }

        // Dieta
        public int? DietaID { get; set; }

        [Required]
        [Display(Name = "Nome da Dieta")]
        public string DietaNome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data da Dieta")]
        public DateTime DataDieta { get; set; }
        public bool Completo { get; set; }
    }
}
