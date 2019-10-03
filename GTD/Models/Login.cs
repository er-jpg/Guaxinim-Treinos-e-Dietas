using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GTD.Models
{
    public class Login
    {
        public int LoginID { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string User { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Lembrar de Mim")]
        public bool Lembrar { get; set; }
    }
}
