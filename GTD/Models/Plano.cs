using GTD.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Models
{
    public class Plano
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

        // FK
        public virtual ICollection<Diario> Diario { get; set; }

        //public int? SemanaID { get; set; } // não está ligado com semana
        //public Semana Semana { get; set; }

        [Required]
        [Display(Name = "Treino")]
        public int? TreinoID { get; set; }
        public Treino Treino { get; set; }

        [Required]
        [Display(Name = "Dieta")]
        public int? DietaID { get; set; }
        public Dieta Dieta { get; set; }
    }
}
