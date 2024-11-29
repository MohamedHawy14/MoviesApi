using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using MoviesApi.Data.Models;
using MoviesApi.DTO;
using MoviesApi.DTO.Movie;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("Movie/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        
        private new List<string> _allowedextention = new List<string> { ".jpg", ".png" };
        private long _maxallowepostersize = 3145728;
        private readonly IGenreServices _genreServices;
        private readonly IMovieServices _movieServices;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext _context,IGenreServices genreServices , IMovieServices movieServices,IMapper mapper)
        {
            this._genreServices = genreServices;
            this._movieServices = movieServices;
            this._mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _movieServices.GetAll();
            var data = _mapper.Map < IEnumerable<MovieDTO>>(movies);
            return Ok(data);
        }
        [HttpGet("{id:int}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            var Movie = await _movieServices.GetById(id);
            if (Movie is null)
                return NotFound();
            var Data = _mapper.Map<MovieDTO>(Movie);
            return Ok(Data);
        }
        [HttpGet("GetMoviesByGenreId")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetMoviesByGenreIdAsync(byte GenreId)
        {
            var movies = await _movieServices.GetAll(GenreId);
            var Data = _mapper.Map<IEnumerable<MovieDTO>>(movies);

            return Ok(Data);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateMovieAsync([FromForm]MovieCreateDTO dTO)
        {
            if (!_allowedextention.Contains(Path.GetExtension(dTO.Poster.FileName).ToLower()))
                return BadRequest("Only .jpg & .png");
            if(dTO.Poster.Length> _maxallowepostersize)
                return BadRequest("Max Allowed Size Is 3Mb");
            var isvalidgenreid = await _genreServices.IsValidGenre(dTO.GenreId);
            if(!isvalidgenreid)
                return BadRequest("Invalid Genre Id");



            using var Datastream = new MemoryStream();
            await dTO.Poster.CopyToAsync(Datastream);
            var movie = _mapper.Map<Movie>(dTO); // from dto to movie
            movie.Poster = Datastream.ToArray(); // important 
            await _movieServices.Add(movie);
            return Ok(movie);
        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditMovie(int id,[FromForm]MovieEditDTO dTO)
        {
            var movie = await _movieServices.GetById(id);
            if (movie == null) return NotFound();
            var isvalidgenreid = await _genreServices.IsValidGenre(dTO.GenreId);
            if (!isvalidgenreid)
                return BadRequest("Invalid Genre Id");
            if (dTO.Poster != null)
            {
                if (!_allowedextention.Contains(Path.GetExtension(dTO.Poster.FileName).ToLower()))
                    return BadRequest("Only .jpg & .png");
                if (dTO.Poster.Length > _maxallowepostersize)
                    return BadRequest("Max Allowed Size Is 3Mb");
                using var Datastream = new MemoryStream();
                await dTO.Poster.CopyToAsync(Datastream);
                movie.Poster = Datastream.ToArray();

            }
            movie.Title = dTO.Title;
            movie.Year = dTO.Year;
            movie.StoryLine = dTO.StoryLine;
            movie.Rate = dTO.Rate;
            movie.GenreId = dTO.GenreId;

            _movieServices.Update(movie);
            return Ok(movie);

        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _movieServices.GetById(id);
            if (movie == null) return NotFound();
            _movieServices.Delete(movie);
            return Ok(movie);
        }
    }
}
