using CodeFirstRadoreOrnek.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstRadoreOrnek.Data
{
    public class ApplicationDbContext : DbContext
    {

        //türemiş olduğumuz class ın constructorına base ile parametre gönderiyoruz 
        //application dbcontext class ımız databasedeki her tablodan sorumlu olacak
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //database deki tablomun adı Kitap olacak

        public DbSet<Kitap> Kitap { get; set; }
    }
}
