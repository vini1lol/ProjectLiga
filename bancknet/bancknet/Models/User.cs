using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace bancknet.Models
{
    public class User
    {
        [Key]
        public int userid { get; set; }
        [DisplayName("Name")]
        [Required]
        public string name { get; set; }
        [DisplayName("Email")]
        [Required]
        public string email { get; set; }
        [DisplayName("Password")]
        [Required]
        public string password { get; set; }
        [Required]
        public double Valor { get; set; }
        public ICollection<Mywallet> Mywallet { get; set; }
    }
}
