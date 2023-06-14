using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Models;

namespace UKParliament.CodeTest.Data.DataContext
{
    public class PersonManagerContext: DbContext
    {
        public PersonManagerContext(DbContextOptions<PersonManagerContext> options) : base(options)
        {
                
        }

        public DbSet<Person> People { get; set; }
    }
}
