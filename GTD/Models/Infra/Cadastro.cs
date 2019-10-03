using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Models.Infra
{
    public class Cadastro
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string User { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} precisa ter ao menos {2} e no máximo {1} caracteres de cumprimento.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "Senha e confirmação não possuem os mesmos valores.")]
        public string ConfirmPassword { get; set; }
    }
}
