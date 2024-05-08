using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class ApplicationDBContext :DbContext // inherit from entity framework core
    {
        // establish connection with entity frmwork core, this is a general setup
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        // [!] for any models inside the db, you need to create a DB set inside the ApplicationDBContext

        // public DbSet<"Model name"> "Table name"
        // in model the name is "Category" => when migrating it will create a table name of Categories automatically, just like in nodejs
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }  
    }
}


