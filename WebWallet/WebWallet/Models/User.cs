using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WebWallet.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("E-mail")]
        [Required]
        public string Email { get; set; }
        [DisplayName("Nome")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Senha")]
        [Required]
        public string Password { get; set; }
    }
}
