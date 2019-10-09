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
        [Required(ErrorMessage = "É obrigatório a seleção de uma semana")]
        public int? SemanaID { get; set; }
        [Display(Name = "Selecione a Semana")]
        public List<SelectListItem> Semanas { get; set; }
        public string Texto { get; set; }

        // Dieta
        public int? DietaID { get; set; }

        [Required]
        [Display(Name = "Nome da Dieta")]
        public string DietaNome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data de Início")]
        public DateTime DataDieta { get; set; }
        public bool Completo { get; set; }
    }
}
