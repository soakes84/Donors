using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackBaudDonorsWebAPI.Models
{
    public class BlackBaudDonorsContext : DbContext
    {
        public BlackBaudDonorsContext(DbContextOptions<BlackBaudDonorsContext> options) : base(options)
        {

        }

        public DbSet<Donor> Donors { get; set; }
    }
}
