using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

//Wallet model
namespace bancknet.Models
{
    public class Mywallet
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Valor")]
        [Required]
        public float Value { get; set; }
        [ForeignKey("User")]
        public int User_id { get; set; }
        public ICollection<Transactions> Transactions { get; set; }

    }
}
