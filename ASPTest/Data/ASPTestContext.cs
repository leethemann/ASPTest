using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ASPTest.Models
{
    public class ASPTestContext : DbContext
    {
        public ASPTestContext (DbContextOptions<ASPTestContext> options)
            : base(options)
        {
        }

        public DbSet<ASPTest.Models.Movie> Movie { get; set; }
    }
}
