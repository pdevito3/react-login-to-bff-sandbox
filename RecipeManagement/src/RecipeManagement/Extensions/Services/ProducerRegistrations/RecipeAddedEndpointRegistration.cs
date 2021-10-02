namespace RecipeManagement.Extensions.Services.ProducerRegistrations
{
    using MassTransit;
    using MassTransit.RabbitMqTransport;
    using Messages;
    using RabbitMQ.Client;

    public static class RecipeAddedEndpointRegistration
    {
        public static void RecipeAddedEndpoint(this IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.Message<IRecipeAdded>(e => e.SetEntityName("recipe-added")); // name of the primary exchange
            cfg.Publish<IRecipeAdded>(e => e.ExchangeType = ExchangeType.Fanout); // primary exchange type
        }
    }
}