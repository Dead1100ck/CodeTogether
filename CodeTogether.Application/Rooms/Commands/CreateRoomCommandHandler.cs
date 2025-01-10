using CodeTogether.Application.Interfaces;
using CodeTogether.Application.Models.Create;
using CodeTogether.DTO;


namespace CodeTogether.Application.Rooms.Commands
{
	public class CreateRoomCommandHandler
	{
		private readonly ICodeTogetherDbContext _dbContext;

		public CreateRoomCommandHandler(ICodeTogetherDbContext dbContext) => _dbContext = dbContext;


		public async Task<Room> HandleAsync(CreateRoomModel request)
		{
			Room newRoom = new Room()
			{
				Type = request.Type,
			};
			await _dbContext.Rooms.AddAsync(newRoom);
			await _dbContext.SaveChangesAsync(CancellationToken.None);
			
			return newRoom;
		}
	}
}
