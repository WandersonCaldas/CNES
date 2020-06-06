using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCNES.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public int cod_usuario { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string txt_usuario { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string txt_email { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string txt_login { get; set; }

        [Required]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string txt_senha { get; set; }
    }
}
