namespace RecipeManagement.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
    using RecipeManagement.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetRecipeTests : TestBase
    {
        [Test]
        public async Task get_recipe_returns_success_when_entity_exists_using_valid_auth_credentials()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            _client.AddAuth(new[] {"recipes.read"});
            
            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.GetRecord.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(200);
        }
            
        [Test]
        public async Task get_recipe_returns_unauthorized_without_valid_token()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.GetRecord.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task get_recipe_returns_forbidden_without_proper_scope()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.GetRecord.Replace(ApiRoutes.Recipes.Id, fakeRecipe.Id.ToString());
            var result = await _client.GetRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}