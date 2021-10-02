namespace RecipeManagement.IntegrationTests.FeatureTests.Recipe
{
    using RecipeManagement.SharedTestHelpers.Fakes.Recipe;
    using RecipeManagement.IntegrationTests.TestUtilities;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using RecipeManagement.Domain.Recipes.Features;
    using static TestFixture;
    using System;
    using Domain.EventHandlers;
    using MassTransit;
    using MassTransit.Testing;
    using Messages;
    using Microsoft.Extensions.DependencyInjection;
    using RecipeManagement.Exceptions;

    public class AddRecipeCommandTests : TestBase
    {
        [Test]
        public async Task can_add_new_recipe_to_db()
        {
            // Arrange
            var fakeRecipeOne = new FakeRecipeForCreationDto { }.Generate();

            // Act
            var command = new AddRecipe.AddRecipeCommand(fakeRecipeOne);
            var recipeReturned = await SendAsync(command);
            var recipeCreated = await ExecuteDbContextAsync(db => db.Recipes.SingleOrDefaultAsync());

            // Assert
            recipeReturned.Should().BeEquivalentTo(fakeRecipeOne, options =>
                options.ExcludingMissingMembers());
            recipeCreated.Should().BeEquivalentTo(fakeRecipeOne, options =>
                options.ExcludingMissingMembers());
        }
        
        [Test]
        public async Task bus_consumer_test()
        {
            // Arrange
            var message = new
            {
                RecipeId = Guid.NewGuid(),
            };

            // Act
            await PublishMessage<IRecipeAdded>(message);

            // Assert
            // did the endpoint consume the message
            (await _harness.Consumed.Any<IRecipeAdded>()).Should().Be(true);

            // ensure that no faults were published by the consumer
            (await _harness.Published.Any<Fault<IRecipeAdded>>()).Should().Be(false);
            
            // the add to book consumer consumed the message
            var consumerHarness = _provider.GetRequiredService<IConsumerTestHarness<AddToBook>>();
            (await consumerHarness.Consumed.Any<IRecipeAdded>()).Should().Be(true);
        }
    }
}