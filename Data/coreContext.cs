using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;

namespace core.Data
{
    public class coreContext : DbContext
    {
        public coreContext (DbContextOptions<coreContext> options)
            : base(options)
        {
        }

        public DbSet<core.Models.DomesticTeamsModel> ClubViewModel { get; set; }
    }
}
