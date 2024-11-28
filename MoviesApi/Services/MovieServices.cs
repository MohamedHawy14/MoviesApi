using MoviesApi.Data;
using MoviesApi.Data.Models;
using MoviesApi.DTO;

namespace MoviesApi.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly ApplicationDbContext context;

        public MovieServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Movie> Add(Movie movie)
        {
            await context.AddAsync(movie);
            context.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            context.Remove(movie);
            context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte GenreId=0)
        {
            return await context.Movies
                .Where(e=>e.GenreId==GenreId || GenreId==0)
                .OrderByDescending(e => e.Rate)
                .Include(e => e.Genre)
                .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await context.Movies.Include(e => e.Genre).SingleOrDefaultAsync(e => e.Id == id);
        }

        public Movie Update(Movie movie)
        {
            context.Update(movie);
            context.SaveChanges();
            return movie;
        }
    }
}
