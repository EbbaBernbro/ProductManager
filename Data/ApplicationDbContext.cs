using Microsoft.EntityFrameworkCore;

namespace ProductManager.Domain;

class ApplicationDbContext : DbContext
{

  //För att ef core ska veta vilken server sql ligger o kör på & vad databasen heter.
  private string connectionString = "Server=.;Database=ProductManager;Trusted_Connection=True;Encrypt=False;";

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(connectionString);
  }
 
  //Product kommer va namnet EF Core förväntar sig att tabellen kommer heta
  //som har samma antal kolumner som det finns publika properties i Product.
  public DbSet<Product> Product { get; set;}


}