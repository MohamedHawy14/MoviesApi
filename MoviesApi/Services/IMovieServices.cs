using MoviesApi.Data.Models;

namespace MoviesApi.Services
{
    public interface IMovieServices
    {
        Task<IEnumerable<Movie>> GetAll(byte GenreId = 0);
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
    }
}
