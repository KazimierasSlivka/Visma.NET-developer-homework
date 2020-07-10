using Microsoft.EntityFrameworkCore;
using ContactManager.Models;

namespace ContactManager.Data
{
    public class ContactManagerDbContext : DbContext
    {
        const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=VismaContactsDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        public DbSet<ContactModel> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
