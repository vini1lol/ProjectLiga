using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Models
{
    public class Usuario : IdentityUser
    {
        public IEnumerable<Carteira> Carteiras { get; set; }
    }
}
