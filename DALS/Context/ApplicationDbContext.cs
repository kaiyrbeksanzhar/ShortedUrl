//Context данных
namespace DALS.Context
{
    //библиотеки(libraries)
    using DALS.Model;
    using System.Data.Entity;

    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ApplicationDbContext : DbContext
    {
        //соединение с config(или с базой)
        public ApplicationDbContext() : base("TestContext")
        { }
        //таблица Url
        public DbSet<Url> Urls { get; set; }
        //таблица ShortUrl
        public DbSet<ShortUrl> shortUrls { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<ShortUrl>()
        //      .HasOptional(a => a.Url)
        //      .WithOptionalDependent()
        //      .WillCascadeOnDelete(true);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
