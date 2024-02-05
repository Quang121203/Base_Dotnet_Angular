using API.Models.Domains;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("/movie/")]

    public class MovieController : ControllerBase
    {


        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

      
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Movie model)
        {
            var update = await movieService.UpdateMovie(model);
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var delete = await movieService.DeleteMovie(id);
            if (delete != null) return Ok(delete);
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll ()
        {
            var movies = await movieService.GetAllMovie();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var movie = await movieService.GetMovie(id);
            return Ok(movie);
        }

        [HttpGet("get/ramdom")]
        public async Task<IActionResult> GetRamdom()
        {
            var movie = await movieService.GetRamdomMovie();
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Movie model)
        {
            var create = await movieService.CreateMovie(model);
            return Ok(create);
        }
    }
}