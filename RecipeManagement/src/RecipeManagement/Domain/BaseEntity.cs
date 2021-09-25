namespace RecipeManagement.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseEntity
    {
        [Key] 
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}