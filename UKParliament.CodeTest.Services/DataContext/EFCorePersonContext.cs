using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Models;

namespace UKParliament.CodeTest.Services.DataContext
{
    public class EFCorePersonContext: DbContext
    {
        public EFCorePersonContext(DbContextOptions<EFCorePersonContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
    }
}
