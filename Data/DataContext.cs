using Microsoft.EntityFrameworkCore;
using Recipes.Api.Models;

namespace Recipes.Api.Data;

/* add migration command cli:
dotnet ef migrations add *migration-name*
*/

/* update database command cli:
dotnet ef database update
*/

/* remove migration command cli:
dotnet ef migrations remove
*/

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    
    //Db Sets:
    public DbSet<Recipe> Recipe { get; set; }

}
