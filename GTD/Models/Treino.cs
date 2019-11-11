using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Models
{
    public class Treino
    {
        public int? TreinoID { get; set; }

        [Required]
        [Display(Name = "Nome do Treino")]
        public string TreinoNome { get; set; }

        //[Required]
        //[DataType(DataType.MultilineText)]
        //[Display(Name = "Treino")]
        //public string DescTreino { get; set; }

        //[Required]
        //[Display(Name = "Duração")]
        //public int Duracao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data do Treino")]
        public DateTime? DataTreino { get; set; } = DateTime.Now;
        public bool Completo { get; set; }

        // FK
        public virtual ICollection<Plano> Plano { get; set; }

        //public int? SemanaID { get; set; }
        //public Semana Semana { get; set; }
        [Display(Name = "Selecione a Semana")]
        public virtual ICollection<TreinoSemana> TreinoSemana { get; set; }
    }
}
