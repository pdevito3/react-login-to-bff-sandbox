namespace RecipeManagement.IntegrationTests.FeatureTests.Recipe
{
    using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
    using RecipeManagement.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipeManagement.Domain.Recipes.Features;
    using static TestFixture;

    public class RecipeQueryTests : TestBase
    {
        [Test]
        public async Task can_get_existing_recipe_with_accurate_props()
        {
            // Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            await InsertAsync(fakeRecipeOne);

            // Act
            var query = new GetRecipe.RecipeQuery(fakeRecipeOne.Id);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().BeEquivalentTo(fakeRecipeOne, options =>
                options.ExcludingMissingMembers());
        }

        [Test]
        public async Task get_recipe_throws_keynotfound_exception_when_record_does_not_exist()
        {
            // Arrange
            var badId = Guid.NewGuid();

            // Act
            var query = new GetRecipe.RecipeQuery(badId);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}