using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Models
{
    public class Semana
    {
        public int? SemanaID { get; set; }

        [Required]
        public int SemanaNum { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data de Início")]
        public DateTime DataInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data de Fim")]
        public DateTime? DataFim { get; set; }

        //[Required]
        //[Display(Name = "Duração")]
        //public int Duracao { get; set; }

        // FK
        public virtual ICollection<Plano> Plano { get; set; }
        public virtual ICollection<Treino> Treino { get; set; }
        public virtual ICollection<Dieta> Dieta { get; set; }
    }
}
