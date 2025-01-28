using CodeTogether.Application.Interfaces;
using CodeTogether.DTO;
using Microsoft.EntityFrameworkCore;


namespace CodeTogether.Application.Rooms.Querries
{
	public class GetRoomByIdQuerryHandler
	{
		private readonly ICodeTogetherDbContext _dbContext;

		public GetRoomByIdQuerryHandler(ICodeTogetherDbContext dbContext) => _dbContext = dbContext;


		public async Task<Room> HandleAsync(Guid id)
		{
			Room room = await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id);

			if (room == null)
				throw new Exception("Bad request");

			return room;
		}
	}
}
