using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebWallet.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Valor em carteira")]
        [Required]
        public float Value { get; set; }
        [ForeignKey("User")]
        public int User_id { get; set; }
    }
}
