using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using MoviesApi.Data.Models;
using MoviesApi.DTO.Genre;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("Genre/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreServices _services;

        public GenresController(IGenreServices services)
        {
            _services = services;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> GetAllGenreAsync()
        {
            var Genres = await _services.GetAll();
            return Ok(Genres);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> CreateGenreAsync(GenreDTO dTO)
        {
            var Genre = new Genre { Name = dTO.Name };
            await _services.Add(Genre);
            return Ok(Genre);
        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditGenreAsync(byte id, GenreDTO dTO)
        {
            var Genre = await _services.GetById(id);
            if (Genre is null)
                return NotFound($"Genre With Id : {id} Not Found");
            Genre.Name = dTO.Name;
           _services.Update(Genre);
            return Ok(Genre);
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteGenreAsync(byte id)
        {
            var Genre = await _services.GetById(id);
            if (Genre is null)
                return NotFound($"Genre With Id : {id} Not Found");
            _services.Delete(Genre);
            return Ok(Genre);

        }




    }
}
