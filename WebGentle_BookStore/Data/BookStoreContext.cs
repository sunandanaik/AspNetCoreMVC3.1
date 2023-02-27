using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebGentle_BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {

        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Language> Language {get; set;}

        //To give ConnectionString here.
        //If you specifying this connection string in Startup.cs file then no need to specify here.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer("Server=.;Database=BookStore;Integrated Security=True;");
        //    base.OnConfiguring(optionBuilder);
        //}
    }
}
