using GTD.Models;
using GTD.Models.Infra;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.ViewModels
{
    public class PlanoViewModel
    {
        public int? PlanoID { get; set; }

        [Required]
        [Display(Name = "Plano")]
        public string PlanoNome { get; set; }

        [Required]
        [Display(Name = "Duração (em semanas)")]
        public int Duracao { get; set; }
        public bool Completo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Semana do Início")]
        public DateTime? SemanaInicio { get; set; }

        [Required]
        [Display(Name = "Selecionado")]
        public bool Selecionado { get; set; }

        // FK parte de login
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int TreinoID { get; set; }
        public int DietaID { get; set; }
        public IEnumerable<SelectListItem> Treinos { get; set; }
        public IEnumerable<SelectListItem> Dietas { get; set; }
    }
}
