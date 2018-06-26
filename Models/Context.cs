using Microsoft.EntityFrameworkCore;

namespace BankAccts.Models{
    public class Context : DbContext{
        public Context(DbContextOptions<Context>options) : base(options) {}
        public DbSet<User> users {get; set;}
        public DbSet<Transaction> transactions { get; set; }
    }
}