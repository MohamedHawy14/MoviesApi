﻿using MoviesApi.Data.Models;

namespace MoviesApi.DTO
{
    public class MovieDTO
    {
        [MaxLength(100)]
        public string Title { get; set; }
        public int Year { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public byte[] Poster { get; set; }
        public byte GenreId { get; set; }

        public string GenreName { get; set; }
    }
}
