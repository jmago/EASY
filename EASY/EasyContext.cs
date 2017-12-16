using EASY.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EASY
{
    public class EasyContext : DbContext
    {
        public EasyContext()
            : base("EasyContext")
        {
            Database.SetInitializer(new SeedInitializer());
        }

        public DbSet<Availability> Availability { get; set; }
        public DbSet<BankInfo> BankInfo { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Knowledge> Knowledge { get; set; }
        public DbSet<KnowledgeXEmployee> KnowledgeXEmployee { get; set; }
        public DbSet<PaypalInfo> PaypalInfo { get; set; }
        public DbSet<WorkingHour> WorkingHour { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}