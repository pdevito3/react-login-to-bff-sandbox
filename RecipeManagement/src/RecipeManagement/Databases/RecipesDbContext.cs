namespace RecipeManagement.Databases
{
    using RecipeManagement.Domain.Recipes;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System.Threading;
    using System.Threading.Tasks;

    public class RecipesDbContext : DbContext
    {
        public RecipesDbContext(
            DbContextOptions<RecipesDbContext> options) : base(options)
        {
        }

        #region DbSet Region - Do Not Delete

        public DbSet<Recipe> Recipes { get; set; }
        #endregion DbSet Region - Do Not Delete

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}