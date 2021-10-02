namespace Messages
{
    using System;

    public interface IRecipeAdded
    {
        public Guid RecipeId { get; set; }
    }
}