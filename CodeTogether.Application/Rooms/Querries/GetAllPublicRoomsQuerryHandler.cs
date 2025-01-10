using CodeTogether.Application.Interfaces;
using CodeTogether.DTO;


namespace CodeTogether.Application.Rooms.Querries
{
	public class GetAllPublicRoomsQuerryHandler
	{
		private readonly ICodeTogetherDbContext _dbContext;

		public GetAllPublicRoomsQuerryHandler(ICodeTogetherDbContext dbContext) => _dbContext = dbContext;


		public async Task<List<Room>> HandleAsync()
		{
			List<Room> Rooms = _dbContext.Rooms.Where(r => r.Type == RoomType.Public).ToList();

			return Rooms;
		}
	}
}
