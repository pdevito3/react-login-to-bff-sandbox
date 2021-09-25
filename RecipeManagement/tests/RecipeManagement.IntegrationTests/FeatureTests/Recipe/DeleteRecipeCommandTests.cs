namespace RecipeManagement.IntegrationTests.FeatureTests.Recipe
{
    using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
    using RecipeManagement.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;
    using RecipeManagement.Domain.Recipes.Features;
    using static TestFixture;

    public class DeleteRecipeCommandTests : TestBase
    {
        [Test]
        public async Task can_delete_recipe_from_db()
        {
            // Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            await InsertAsync(fakeRecipeOne);
            var recipe = await ExecuteDbContextAsync(db => db.Recipes.SingleOrDefaultAsync());
            var id = recipe.Id;

            // Act
            var command = new DeleteRecipe.DeleteRecipeCommand(id);
            await SendAsync(command);
            var recipeResponse = await ExecuteDbContextAsync(db => db.Recipes.ToListAsync());

            // Assert
            recipeResponse.Count.Should().Be(0);
        }

        [Test]
        public async Task delete_recipe_throws_keynotfoundexception_when_record_does_not_exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var command = new DeleteRecipe.DeleteRecipeCommand(badId);
            Func<Task> act = () => SendAsync(command);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}