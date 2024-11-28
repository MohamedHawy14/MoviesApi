namespace MoviesApi.DTO.Movie
{
    public class MovieEditDTO:MovieDetailsDTO
    {
        public IFormFile? Poster { get; set; }
    }
}
