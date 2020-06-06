using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCNES.Models
{
    public class User
    {
        [Required]        
        public string Username { get; set; }

        [Required]        
        public string Password { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
