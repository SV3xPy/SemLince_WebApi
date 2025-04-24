using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SemLince_Domain;

namespace SemLince_Infrastructure.EntityFramework
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

    }
}
