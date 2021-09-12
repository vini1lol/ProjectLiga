using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Models
{
    public class Carteira : Entity
    {

        [Required(ErrorMessage = "O {0} é obrigatorio")]
        [StringLength(200, ErrorMessage = "O campo {0} deve ter entre {2} e {1}", MinimumLength = 2)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        public decimal Valor { get; set; }
        [DisplayName("Ativa?")]
        public bool Ativo { get; set; }

        public Usuario Usuario { get; set; }
        public IEnumerable<Movimentacao> Movimentacoes { get; set; }
    }
}
