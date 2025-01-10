using Microsoft.AspNetCore.Mvc;

using CodeTogether.DB;
using CodeTogether.DTO;
using CodeTogether.WebAPI.Models.Create;


namespace CodeTogether.WebAPI.Controllers
{
	[Route("api/[controller]")]
	public class RoomController : BaseController
	{
		public RoomController(CodeTogetherDbContext dbContext) : base(dbContext) { }


		[HttpGet]
		public async Task<List<Room>> GetAll()
		{
			return _dbContext.Rooms.Where(r => r.Type == RoomType.Public).ToList();
		}

		[HttpPost]
		public async Task<Room> CreateRoom(CreateRoomModel createRoomModel)
		{
			Room newRoom = new Room { Type = createRoomModel.Type };
			await _dbContext.Rooms.AddAsync(newRoom);
			await _dbContext.SaveChangesAsync();

			return newRoom;
		}
	}
}
