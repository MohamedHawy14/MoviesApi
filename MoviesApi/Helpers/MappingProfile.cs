using AutoMapper;
using MoviesApi.Data.Models;
using MoviesApi.DTO;
using MoviesApi.DTO.Movie;

namespace MoviesApi.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDTO>(); // from Movie to MovieDTO
            CreateMap<Movie, MovieCreateDTO>();
            CreateMap<Movie, MovieEditDTO>();
            CreateMap<MovieDTO, Movie>();
            CreateMap<MovieCreateDTO, Movie>().ForMember(src=>src.Poster,opt=>opt.Ignore());
            CreateMap<MovieEditDTO, Movie>();

        }
    }
}
