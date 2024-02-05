using API.Models.Domains;

namespace API.Services.Interfaces
{
    public interface IMovieService
    {
        public Task<object> UpdateMovie(Movie model);
        public Task<object?> DeleteMovie(int id);
        public Task<object?> GetMovie(int id);
        public Task<object?> GetAllMovie();
        public Task<object> GetRamdomMovie();
        public Task<object> CreateMovie(Movie model);

    }
}
