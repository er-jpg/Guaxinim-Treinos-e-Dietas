using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.ViewModels
{
    public class TreinoSemanaViewModel
    {
        [Required(ErrorMessage = "É obrigatório a seleção de uma semana")]
        public int? SemanaID { get; set; }
        [Display(Name = "Selecione a Semana")]
        public List<SelectListItem> Semanas { get; set; }
        public string Texto { get; set; }

        // Treino
        public int? TreinoID { get; set; }

        [Required]
        [Display(Name = "Nome do Treino")]
        public string TreinoNome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data de Início")]
        public DateTime DataTreino { get; set; }
        public bool Completo { get; set; }
    }
}
