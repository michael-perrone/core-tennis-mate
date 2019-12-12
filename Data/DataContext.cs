using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tennis_Mate.Models;
using Microsoft.EntityFrameworkCore;


namespace Tennis_Mate.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        public DbSet<Value> Values { get; set; }
        // table name when scaffolding databse
        public DbSet<User> Users { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
    }
}
