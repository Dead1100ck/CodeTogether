using Microsoft.AspNetCore.Mvc;

using CodeTogether.DB;
using CodeTogether.DTO;
using CodeTogether.Application.Rooms.Commands;
using CodeTogether.Application.Rooms.Querries;
using CodeTogether.Application.Models.Requests.Rooms.Create;


namespace CodeTogether.WebAPI.Controllers
{
    [Route("api/[controller]")]
	public class RoomController : BaseController
	{
		public RoomController(CodeTogetherDbContext dbContext) : base(dbContext) { }


		[HttpGet]
		public async Task<ActionResult<List<Room>>> GetAll()
		{
			var querry = new GetAllPublicRoomsQuerryHandler(_dbContext);
			var response = await querry.HandleAsync();

			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<Room>> CreateRoom([FromBody] CreateRoomModel createRoomModel)
		{
			var command = new CreateRoomCommandHandler(_dbContext);
			var response = await command.HandleAsync(createRoomModel);

			return Ok(response);
		}
	}
}
