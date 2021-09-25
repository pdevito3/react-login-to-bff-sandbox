namespace RecipeManagement.Domain.Recipes.Features
{
    using RecipeManagement.Dtos.Recipe;
    using RecipeManagement.Exceptions;
    using RecipeManagement.Databases;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class GetRecipe
    {
        public class RecipeQuery : IRequest<RecipeDto>
        {
            public Guid Id { get; set; }

            public RecipeQuery(Guid id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<RecipeQuery, RecipeDto>
        {
            private readonly RecipesDbContext _db;
            private readonly IMapper _mapper;

            public Handler(RecipesDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<RecipeDto> Handle(RecipeQuery request, CancellationToken cancellationToken)
            {
                var result = await _db.Recipes
                    .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(r => r.Id == request.Id);

                if (result == null)
                    throw new KeyNotFoundException();

                return result;
            }
        }
    }
}