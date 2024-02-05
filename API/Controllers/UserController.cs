using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("/user/")]
    [Authorize]
    public class UserController : ControllerBase
    {


        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserVM model)
        {
            var update = await userService.UpdateUser(model);
            if (update != null) return Ok(update);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var delete = await userService.DeleteUser(id);
            if (delete != null) return Ok(delete);
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll ()
        {
            var users = await userService.GetAllUser();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var user = await userService.GetUser(id);
            return Ok(user);
        }

        [HttpGet("get/stat")]
        public async Task<IActionResult> Stat()
        {
            var user = await userService.GetUserStats();
            return Ok(user);
        }
    }
}