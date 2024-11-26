using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using MoviesApi.Data.Models;
using MoviesApi.DTO;

namespace MoviesApi.Controllers
{
    [Route("Movie/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private new List<string> _allowedextention = new List<string> { ".jpg",".png"};
        private long _maxallowepostersize = 3145728;

        public MoviesController(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpPost]
        public async Task<ActionResult> CreateMovieAsync([FromForm]MovieDTO dTO)
        {
            if (!_allowedextention.Contains(Path.GetExtension(dTO.Poster.FileName).ToLower()))
                return BadRequest("Only .jpg & .png");
            if(dTO.Poster.Length> _maxallowepostersize)
                return BadRequest("Max Allowed Size Is 3Mb");
            var isvalidgenreid = await context.Genres.AnyAsync(e => e.Id==dTO.GenreId);
            if(!isvalidgenreid)
                return BadRequest("Invalid Genre Id");



            using var Datastream = new MemoryStream();
            await dTO.Poster.CopyToAsync(Datastream);
            var movie = new Movie
            {
                Title = dTO.Title,
                Year = dTO.Year,
                Poster = Datastream.ToArray(),
                GenreId = dTO.GenreId,
                Rate = dTO.Rate,
                StoryLine = dTO.StoryLine
            };
            await context.AddAsync(movie);
            context.SaveChanges();
            return Ok(movie);
        }
    }
}
