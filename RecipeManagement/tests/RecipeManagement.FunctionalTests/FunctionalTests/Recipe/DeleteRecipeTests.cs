namespace RecipeManagement.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
    using RecipeManagement.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class DeleteRecipeTests : TestBase
    {
        [Test]
        public async Task delete_recipe_returns_nocontent_when_entity_exists_and_auth_credentials_are_valid()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            _client.AddAuth(new[] {"recipes.delete"});
            
            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Delete.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(204);
        }
            
        [Test]
        public async Task delete_recipe_returns_unauthorized_without_valid_token()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Delete.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task delete_recipe_returns_forbidden_without_proper_scope()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Delete.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}