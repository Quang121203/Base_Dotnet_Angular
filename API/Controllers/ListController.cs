using API.Models.Domains;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("/list/")]

    public class ListController : ControllerBase
    {


        private readonly IListService listService;

        public ListController(IListService listService)
        {
            this.listService = listService;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var delete = await listService.DeleteList(id);
            if (delete != null) return Ok(delete);
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? genre, [FromQuery] string? type)
        {
            if (type!=null)
            {
                if(genre!=null)
                {
                    return Ok(await listService.GetList(genre, type));
                }
                return Ok(await listService.GetList("null", type));
            }
            return Ok(await listService.GetList("null", "null"));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List model)
        {
            var update = await listService.CreateList(model);
            return Ok(update);
        }
    }
}