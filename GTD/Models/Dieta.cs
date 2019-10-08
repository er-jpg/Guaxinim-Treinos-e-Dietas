using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Models
{
    public class Dieta
    {
        public int? DietaID { get; set; }

        [Required]
        [Display(Name = "Nome da Dieta")]
        public string DietaNome { get; set; }

        //[Required]
        //[DataType(DataType.MultilineText)]
        //[Display(Name = "Dieta")]
        //public string DescDieta { get; set; }

        //[Required]
        //[Display(Name = "Duração")]
        //public int Duracao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data da Dieta")]
        public DateTime DataDieta { get; set; }
        public bool Completo { get; set; }

        // FK
        public virtual ICollection<Plano> Plano { get; set; }

        //[Display(Name = "Selecione a Semana")]
        //public int? SemanaID { get; set; }
        //public Semana Semana { get; set; }
        [Display(Name = "Selecione a Semana")]
        public virtual ICollection<DietaSemana> DietaSemana { get; set; }
    }
}
