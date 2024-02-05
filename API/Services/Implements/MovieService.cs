using API.DataAccess;
using API.Models.Domains;
using API.Services.Interfaces;

namespace API.Services.Implements
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork unitOfWork;
        public MovieService(IUnitOfWork unitOfWork) { 
            this.unitOfWork = unitOfWork;
        }

        public async Task<object> CreateMovie(Movie model)
        {
            await unitOfWork.MovieRepository.InsertAsync(model);
            await unitOfWork.SaveChangesAsync();
            return (new
            {
                EM= "create successfully",
                EC=0,
                DT=""
            });
        }

        public async Task<object?> DeleteMovie(int id)
        {
            var movie = await unitOfWork.MovieRepository.GetSingleAsync(id);
            if(movie != null)
            {
                var delete= await unitOfWork.MovieRepository.DeleteAsync(movie.Id);
                await unitOfWork.SaveChangesAsync();
                if (delete)
                {
                    return (new
                    {
                        EM = "delete successfully",
                        EC = 0,
                        DT = ""
                    });
                }
                return null;
            }
            return (new
            {
                EM= "not found this movie",
                EC=1,
                DT=""
            });
        }

        public async Task<object?> GetAllMovie()
        {
            var movies = await unitOfWork.MovieRepository.GetAsync();
            return (new
            {
                EM = "",
                EC = 0,
                DT = movies
            });
        }

        public async Task<object?> GetMovie(int id)
        {
            var movie = await unitOfWork.MovieRepository.GetSingleAsync(id);
            if (movie != null)
            {

                return (new
                {
                    EM = "",
                    EC = 0,
                    DT = movie
                });

            }
            return (new
            {
                EM = "not found this movie",
                EC = 1,
                DT = ""
            });
        }

        public async Task<object> GetRamdomMovie()
        {
            var movie = await unitOfWork.MovieRepository.Random(1);

            return (new
            {
                EM = "",
                EC = 0,
                DT = movie
            });
        }

        public async Task<object> UpdateMovie(Movie model)
        {
            var movie = await unitOfWork.MovieRepository.GetSingleAsync(model.Id);
            if (movie != null)
            {
                movie.Title = model.Title;
                movie.Desc= model.Desc;
                movie.Img = model.Img;
                movie.ImgTitle = model.ImgTitle;
                movie.Trailer = model.Trailer;
                movie.Video= model.Video;
                movie.Year = model.Year;
                movie.Limit = model.Limit;
                movie.Genre = model.Genre;
                movie.IsSeries = model.IsSeries;

                unitOfWork.MovieRepository.Update(movie);
                await unitOfWork.SaveChangesAsync();

                return (new
                {
                    EM = "update successfully",
                    EC = 0,
                    DT = ""
                });

            }
            return (new
            {
                EM = "not found this movie",
                EC = 1,
                DT = ""
            });
        }
    }
}
