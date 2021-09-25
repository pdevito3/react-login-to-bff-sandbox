namespace RecipeManagement.FunctionalTests.FunctionalTests.Recipe
{
    using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
    using RecipeManagement.FunctionalTests.TestUtilities;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CreateRecipeTests : TestBase
    {
        [Test]
        public async Task create_recipe_returns_created_using_valid_dto_and_valid_auth_credentials()
        {
            // Arrange
            var fakeRecipe = new FakeRecipeForCreationDto { }.Generate();

            _client.AddAuth(new[] {"recipes.add"});

            // Act
            var route = ApiRoutes.Recipes.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeRecipe);

            // Assert
            result.StatusCode.Should().Be(201);
        }
            
        [Test]
        public async Task create_recipe_returns_unauthorized_without_valid_token()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeRecipe);

            // Assert
            result.StatusCode.Should().Be(401);
        }
            
        [Test]
        public async Task create_recipe_returns_forbidden_without_proper_scope()
        {
            // Arrange
            var fakeRecipe = new FakeRecipe { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeRecipe);

            // Act
            var route = ApiRoutes.Recipes.Create;
            var result = await _client.PostJsonRequestAsync(route, fakeRecipe);

            // Assert
            result.StatusCode.Should().Be(403);
        }
    }
}