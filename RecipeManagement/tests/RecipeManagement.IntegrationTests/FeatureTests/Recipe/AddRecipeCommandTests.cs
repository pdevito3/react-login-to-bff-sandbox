namespace RecipeManagement.IntegrationTests.FeatureTests.Recipe
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using System;
    using Domain.EventHandlers;
    using MassTransit;
    using MassTransit.Testing;
    using Messages;
    using Microsoft.Extensions.DependencyInjection;
    using static TestFixture;
    using Moq;

    public class AddRecipeCommandTests : TestBase
    {
        [Test]
        public async Task bus_consumer_test()
        {
            // Arrange
            var message = new Mock<IRecipeAdded>();

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