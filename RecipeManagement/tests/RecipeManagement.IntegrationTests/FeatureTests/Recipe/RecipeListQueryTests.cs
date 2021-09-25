namespace RecipeManagement.IntegrationTests.FeatureTests.Recipe
{
    using RecipeManagement.Dtos.Recipe;
    using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
    using RecipeManagement.Exceptions;
    using RecipeManagement.Domain.Recipes.Features;
    using FluentAssertions;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static TestFixture;

    public class RecipeListQueryTests : TestBase
    {
        
        [Test]
        public async Task can_get_recipe_list()
        {
            // Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            var queryParameters = new RecipeParametersDto();

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            // Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(2);
        }
        
        [Test]
        public async Task can_get_recipe_list_with_expected_page_size_and_number()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            var fakeRecipeThree = new FakeRecipe { }.Generate();
            var queryParameters = new RecipeParametersDto() { PageSize = 1, PageNumber = 2 };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo, fakeRecipeThree);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
        }
        
        [Test]
        public async Task get_recipe_list_throws_apiexception_when_query_parameters_are_null()
        {
            // Arrange
            // N/A

            // Act
            var query = new GetRecipeList.RecipeListQuery(null);
            Func<Task> act = () => SendAsync(query);

            // Assert
            act.Should().Throw<ApiException>();
        }
        
        [Test]
        public async Task can_get_sorted_list_of_recipe_by_Title_in_asc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Title = "bravo";
            fakeRecipeTwo.Title = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "Title" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_get_sorted_list_of_recipe_by_Title_in_desc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Title = "alpha";
            fakeRecipeTwo.Title = "bravo";
            var queryParameters = new RecipeParametersDto() { SortOrder = "-Title" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_get_sorted_list_of_recipe_by_Directions_in_asc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Directions = "bravo";
            fakeRecipeTwo.Directions = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "Directions" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_get_sorted_list_of_recipe_by_Directions_in_desc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Directions = "alpha";
            fakeRecipeTwo.Directions = "bravo";
            var queryParameters = new RecipeParametersDto() { SortOrder = "-Directions" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_get_sorted_list_of_recipe_by_RecipeSourceLink_in_asc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.RecipeSourceLink = "bravo";
            fakeRecipeTwo.RecipeSourceLink = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "RecipeSourceLink" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_get_sorted_list_of_recipe_by_RecipeSourceLink_in_desc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.RecipeSourceLink = "alpha";
            fakeRecipeTwo.RecipeSourceLink = "bravo";
            var queryParameters = new RecipeParametersDto() { SortOrder = "-RecipeSourceLink" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_get_sorted_list_of_recipe_by_Description_in_asc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Description = "bravo";
            fakeRecipeTwo.Description = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "Description" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_get_sorted_list_of_recipe_by_Description_in_desc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Description = "alpha";
            fakeRecipeTwo.Description = "bravo";
            var queryParameters = new RecipeParametersDto() { SortOrder = "-Description" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_get_sorted_list_of_recipe_by_ImageLink_in_asc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.ImageLink = "bravo";
            fakeRecipeTwo.ImageLink = "alpha";
            var queryParameters = new RecipeParametersDto() { SortOrder = "ImageLink" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_get_sorted_list_of_recipe_by_ImageLink_in_desc_order()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.ImageLink = "alpha";
            fakeRecipeTwo.ImageLink = "bravo";
            var queryParameters = new RecipeParametersDto() { SortOrder = "-ImageLink" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
            recipes
                .Skip(1)
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeOne, options =>
                    options.ExcludingMissingMembers());
        }

        
        [Test]
        public async Task can_filter_recipe_list_using_Title()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Title = "alpha";
            fakeRecipeTwo.Title = "bravo";
            var queryParameters = new RecipeParametersDto() { Filters = $"Title == {fakeRecipeTwo.Title}" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_filter_recipe_list_using_Directions()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Directions = "alpha";
            fakeRecipeTwo.Directions = "bravo";
            var queryParameters = new RecipeParametersDto() { Filters = $"Directions == {fakeRecipeTwo.Directions}" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_filter_recipe_list_using_RecipeSourceLink()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.RecipeSourceLink = "alpha";
            fakeRecipeTwo.RecipeSourceLink = "bravo";
            var queryParameters = new RecipeParametersDto() { Filters = $"RecipeSourceLink == {fakeRecipeTwo.RecipeSourceLink}" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_filter_recipe_list_using_Description()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.Description = "alpha";
            fakeRecipeTwo.Description = "bravo";
            var queryParameters = new RecipeParametersDto() { Filters = $"Description == {fakeRecipeTwo.Description}" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
        }

        [Test]
        public async Task can_filter_recipe_list_using_ImageLink()
        {
            //Arrange
            var fakeRecipeOne = new FakeRecipe { }.Generate();
            var fakeRecipeTwo = new FakeRecipe { }.Generate();
            fakeRecipeOne.ImageLink = "alpha";
            fakeRecipeTwo.ImageLink = "bravo";
            var queryParameters = new RecipeParametersDto() { Filters = $"ImageLink == {fakeRecipeTwo.ImageLink}" };

            await InsertAsync(fakeRecipeOne, fakeRecipeTwo);

            //Act
            var query = new GetRecipeList.RecipeListQuery(queryParameters);
            var recipes = await SendAsync(query);

            // Assert
            recipes.Should().HaveCount(1);
            recipes
                .FirstOrDefault()
                .Should().BeEquivalentTo(fakeRecipeTwo, options =>
                    options.ExcludingMissingMembers());
        }

    }
}