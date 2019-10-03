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
        public int PlanoNome { get; set; }

        [Required]
        [Display(Name = "Duração")]
        public int Duracao { get; set; }
        public bool Completo { get; set; }

        // FK parte de login
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        // FK
        public virtual ICollection<Diario> Diario { get; set; }

        public int? SemanaID { get; set; }
        public Semana Semana { get; set; }

        public int? TreinoID { get; set; }
        public Treino Treino { get; set; }

        public int? DietaID { get; set; }
        public Dieta Dieta { get; set; }
    }
}
