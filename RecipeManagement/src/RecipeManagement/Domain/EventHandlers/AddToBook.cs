namespace RecipeManagement.Domain.EventHandlers
{
    using AutoMapper;
    using MassTransit;
    using Messages;
    using System.Threading.Tasks;
    using RecipeManagement.Databases;

    public class AddToBook : IConsumer<IRecipeAdded>
    {
        private readonly IMapper _mapper;
        private readonly RecipesDbContext _db;

        public AddToBook(RecipesDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public class AddToBookProfile : Profile
        {
            public AddToBookProfile()
            {
                //createmap<to this, from this>
            }
        }

        public Task Consume(ConsumeContext<IRecipeAdded> context)
        {
            // do work here

            return Task.CompletedTask;
        }
    }
}