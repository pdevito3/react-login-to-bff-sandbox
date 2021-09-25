namespace RecipeManagement.Domain.Recipes.Features
{
    using RecipeManagement.Domain.Recipes;
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

    public static class DeleteRecipe
    {
        public class DeleteRecipeCommand : IRequest<bool>
        {
            public Guid Id { get; set; }

            public DeleteRecipeCommand(Guid recipe)
            {
                Id = recipe;
            }
        }

        public class Handler : IRequestHandler<DeleteRecipeCommand, bool>
        {
            private readonly RecipesDbContext _db;
            private readonly IMapper _mapper;

            public Handler(RecipesDbContext db, IMapper mapper)
            {
                _mapper = mapper;
                _db = db;
            }

            public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
            {
                var recordToDelete = await _db.Recipes
                    .FirstOrDefaultAsync(r => r.Id == request.Id);

                if (recordToDelete == null)
                    throw new KeyNotFoundException();

                _db.Recipes.Remove(recordToDelete);
                await _db.SaveChangesAsync();

                return true;
            }
        }
    }
}