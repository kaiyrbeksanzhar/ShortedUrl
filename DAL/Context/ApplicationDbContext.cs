//DBContext соединение с базой(connectionString)
namespace DAL.Context
{
    //Библиотеки 
    using DALS.Model;
    using MySql.Data.EntityFramework;
    using System.Data.Entity;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=TextContext")
        {
             this.Configuration.ValidateOnSaveEnabled = false;
        }
      
        //Таблица Url
        public DbSet<Url> Urls { get; set; }
        public DbSet<ShortUrl> shortUrls { get; set; }

    }
}
