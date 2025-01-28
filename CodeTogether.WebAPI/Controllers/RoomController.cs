using Microsoft.AspNetCore.Mvc;

using CodeTogether.DB;
using CodeTogether.DTO;
using CodeTogether.Application.Rooms.Commands;
using CodeTogether.Application.Rooms.Querries;
using CodeTogether.Application.Models.Requests.Rooms.Create;
using Microsoft.AspNetCore.Authorization;
using CodeTogether.Application.Models.Responses.Rooms;


namespace CodeTogether.WebAPI.Controllers
{
    [Route("api/[controller]")]
	[Authorize]
	public class RoomController : BaseController
	{
		public RoomController(CodeTogetherDbContext dbContext) : base(dbContext) { }


		[HttpGet]
		public async Task<ActionResult<Room>> Room([FromQuery] RoomResponseModel room)
		{
			var request = new GetRoomByIdQuerryHandler(_dbContext);
			var response = await request.HandleAsync(room.Id);

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
