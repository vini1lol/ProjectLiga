using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

//User model
namespace bancknet.Models
{
    public class User
    {
        [Key]
        public int userid { get; set; }
        [DisplayName("Nome")]
        [Required]
        public string name { get; set; }
        [DisplayName("Email")]
        [Required]
        public string email { get; set; }
        [DisplayName("Senha")]
        [Required]
        public string password { get; set; }
        public ICollection<Mywallet> Mywallet { get; set; }
    }
}
