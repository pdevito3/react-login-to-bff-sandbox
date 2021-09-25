namespace RecipeManagement.Domain.Recipes.Features
{
    using RecipeManagement.Domain.Recipes;
    using RecipeManagement.Dtos.Recipe;
    using RecipeManagement.Exceptions;
    using RecipeManagement.Databases;
    using RecipeManagement.Domain.Recipes.Validators;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class AddRecipe
    {
        public class AddRecipeCommand : IRequest<RecipeDto>
        {
            public RecipeForCreationDto RecipeToAdd { get; set; }

            public AddRecipeCommand(RecipeForCreationDto recipeToAdd)
            {
                RecipeToAdd = recipeToAdd;
            }
        }

        public class Handler : IRequestHandler<AddRecipeCommand, RecipeDto>
        {
            private readonly RecipesDbContext _db;
            private readonly IMapper _mapper;

            public Handler(RecipesDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<RecipeDto> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
            {
                var recipe = _mapper.Map<Recipe> (request.RecipeToAdd);
                _db.Recipes.Add(recipe);

                await _db.SaveChangesAsync();

                return await _db.Recipes
                    .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(r => r.Id == recipe.Id);
            }
        }
    }
}