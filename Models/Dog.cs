using System;
using System.ComponentModel.DataAnnotations;

namespace Dog.Models
{
    public class Dog
    {
        public int DogId {get;set;}
        [Required]
        public string Name {get;set;}
        public string Breed {get;set;}
        public double Weight {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}