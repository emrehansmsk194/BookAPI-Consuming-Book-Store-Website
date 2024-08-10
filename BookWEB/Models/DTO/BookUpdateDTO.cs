﻿using System.ComponentModel.DataAnnotations;

namespace BookWEB.Models.DTO
{
    public class BookUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int PageCount { get; set; }
        public string CoverImageUrl { get; set; }
        public string Language { get; set; }
        [Required]
        public int PublisherId { get; set; }
    }
}
