namespace RecipeManagement.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
    using RecipeManagement.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GetRecipeListTests : TestBase
    {
        [Test]
        public async Task get_recipe_list_returns_success_using_valid_auth_credentials()
        {
            // Arrange
            _client.AddAuth(new[] {"recipes.read"});

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Recipes.GetList);

            // Assert
            result.StatusCode.Should().Be(200);
        }
            
        [Test]
        public async Task get_recipe_list_returns_unauthorized_without_valid_token()
        {
            // Arrange
            // N/A

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Recipes.GetList);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task get_recipe_list_returns_forbidden_without_proper_scope()
        {
            // Arrange
            _client.AddAuth();

            // Act
            var result = await _client.GetRequestAsync(ApiRoutes.Recipes.GetList);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}