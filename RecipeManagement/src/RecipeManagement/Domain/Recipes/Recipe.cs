namespace RecipeManagement.Domain.Recipes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Sieve.Attributes;

    [Table("Recipe")]
    public class Recipe : BaseEntity
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string Title { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Directions { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string RecipeSourceLink { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string Description { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public string ImageLink { get; set; }
    }
}