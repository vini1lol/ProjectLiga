using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Models;

namespace WebWallet.Data
{
    public class AplicatinsDbContext : DbContext
    {
        public AplicatinsDbContext(DbContextOptions<AplicatinsDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}
