namespace RecipeManagement.SharedTestHelpers.Fakes.Recipe
{
    using AutoBogus;
    using RecipeManagement.Domain.Recipes;

    // or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
    public class FakeRecipe : AutoFaker<Recipe>
    {
        public FakeRecipe()
        {
            // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
            //RuleFor(r => r.ExampleIntProperty, r => r.Random.Number(50, 100000));
            //RuleFor(r => r.ExampleDateProperty, r => r.Date.Past());
        }
    }
}