namespace RecipeManagement.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
    using RecipeManagement.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class UpdateRecipeRecordTests : TestBase
    {
        [Test]
        public async Task put_recipe_returns_nocontent_when_entity_exists_and_auth_credentials_are_valid()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            var updatedRecipeDto = new FakeRecipeForUpdateDto { }.Generate();

            _client.AddAuth(new[] {"recipes.update"});
            
            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Put.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedRecipeDto);

            // Assert
            result.StatusCode.Should().Be(204);
        }
            
        [Test]
        public async Task put_recipe_returns_unauthorized_without_valid_token()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            var updatedRecipeDto = new FakeRecipeForUpdateDto { }.Generate();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Put.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedRecipeDto);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task put_recipe_returns_forbidden_without_proper_scope()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            var updatedRecipeDto = new FakeRecipeForUpdateDto { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Put.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.PutJsonRequestAsync(route, updatedRecipeDto);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}