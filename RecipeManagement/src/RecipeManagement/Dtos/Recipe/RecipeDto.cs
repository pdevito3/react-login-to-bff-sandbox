namespace RecipeManagement.Dtos.Recipe
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RecipeDto 
    {

        public Guid Id { get; set; }        public string Title { get; set; }
        public string Directions { get; set; }
        public string RecipeSourceLink { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
    }
}