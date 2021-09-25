namespace RecipeManagement.Seeders.DummyData
{

    using AutoBogus;
    using RecipeManagement.Domain.Recipes;
    using RecipeManagement.Databases;
    using System.Linq;

    public static class RecipeSeeder
    {
        public static void SeedSampleRecipeData(RecipesDbContext context)
        {
            if (!context.Recipes.Any())
            {
                context.Recipes.Add(new AutoFaker<Recipe>());
                context.Recipes.Add(new AutoFaker<Recipe>());
                context.Recipes.Add(new AutoFaker<Recipe>());

                context.SaveChanges();
            }
        }
    }
}