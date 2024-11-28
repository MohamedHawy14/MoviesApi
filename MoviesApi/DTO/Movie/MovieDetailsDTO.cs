namespace MoviesApi.DTO.Movie
{
    public class MovieDetailsDTO
    {
        [MaxLength(100)]
        public string Title { get; set; }
        public int Year { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public byte GenreId { get; set; }
        
    }
}
