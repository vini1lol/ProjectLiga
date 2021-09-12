using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Models
{
    public class Movimentacao : Entity
    {
        public Guid CarteiraId { get; set; }
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        [StringLength(1000, ErrorMessage = "O campo {0} deve ter entre {2} e {1}", MinimumLength = 2)]
        [DisplayName("Descrição")]
        public string Descrip { get; set; }
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        public decimal Valor { get; set; }

        public Carteira Carteira { get; set; }
    }
}
