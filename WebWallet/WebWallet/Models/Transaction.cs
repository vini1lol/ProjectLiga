using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebWallet.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public string Date { get; set; }
        [DisplayName("Descrição")]
        [Required]
        public string Describ { get; set; }
        [DisplayName("Valor")]
        [Required]
        public float Value { get; set; }
        [ForeignKey("Wallet")]
        public int Wallet_id { get; set; }
    }
}
