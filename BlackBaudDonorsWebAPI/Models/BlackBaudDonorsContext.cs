using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackBaudDonorsWebAPI.Models
{
    public class BlackBaudDonorContext : DbContext
    {
        public BlackBaudDonorContext(DbContextOptions<BlackBaudDonorContext> options) : base(options)
        {

        }

        public DbSet<Donor> Donors { get; set; }
    }
}
