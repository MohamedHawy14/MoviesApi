namespace MoviesApi.DTO.Movie
{
    public class MovieCreateDTO:MovieDetailsDTO
    {
        public IFormFile Poster { get; set; }
    }
}
