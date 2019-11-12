using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.ViewModels
{
    public class DiarioViewModel
    {
        [Display(Name = "Nome do Plano")]
        public string PlanoNome { get; set; }

        [Display(Name = "Treino Completo")]
        public bool CompletoTreino { get; set; }
        public string Treino { get; set; }

        [Display(Name = "Dieta Completa")]
        public bool CompletoDieta { get; set; }
        public string Dieta { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DataDiario { get; set; }
    }
}
