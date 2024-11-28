using MoviesApi.Data;
using MoviesApi.Data.Models;

namespace MoviesApi.Services
{
    public class GenreServices : IGenreServices
    {
        private readonly ApplicationDbContext context;

        public GenreServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Genre> Add(Genre genre)
        {
            await context.Genres.AddAsync(genre);
            context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre )
        {
            context.Genres.Remove(genre);
            context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return  await context.Genres.OrderBy(e => e.Name).ToListAsync();
        }

        public async Task<Genre> GetById(byte id)
        {
            return await context.Genres.SingleOrDefaultAsync(e => e.Id == id); 
        }

        public async Task<bool> IsValidGenre(byte id)
        {
           return await context.Genres.AnyAsync(e => e.Id == id);
        }

        public Genre Update(Genre genre)
        {
            context.Genres.Update(genre);
            context.SaveChanges();
            return genre;
        }
    }
}
