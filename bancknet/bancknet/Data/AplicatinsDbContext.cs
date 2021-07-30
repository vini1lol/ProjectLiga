using bancknet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bancknet.Data
{
    public class AplicatinsDbContext: DbContext
    {
        public AplicatinsDbContext(DbContextOptions<AplicatinsDbContext> options ) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Mywallet> Mywallet { get; set; }
    }
}
