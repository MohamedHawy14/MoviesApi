using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using MoviesApi.Data.Models;
using MoviesApi.DTO;

namespace MoviesApi.Controllers
{
    [Route("Genre/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GenresController(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllGenreAsync()
        {
            var Genres = await context.Genres.OrderBy(e=>e.Name).ToListAsync();
            return Ok(Genres);
        }
        [HttpPost]
        public async Task<ActionResult> CreateGenreAsync(GenreDTO dTO)
        {
            var Genre = new Genre { Name = dTO.Name };
            await context.Genres.AddAsync(Genre);
            context.SaveChanges();
            return Ok(Genre);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> EditGenreAsync(int id, GenreDTO dTO)
        {
            var Genre = await context.Genres.SingleOrDefaultAsync(e => e.Id == id);
            if (Genre is null)
                return NotFound($"Genre With Id : {id} Not Found");
            Genre.Name = dTO.Name;
            context.Update(Genre);
            context.SaveChanges();
            return Ok(Genre);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteGenreAsync(int id)
        {
            var Genre = await context.Genres.SingleOrDefaultAsync(e => e.Id == id);
            if (Genre is null)
                return NotFound($"Genre With Id : {id} Not Found");
            context.Remove(Genre);
            context.SaveChanges();
            return Ok(Genre);

        }




    }
}
