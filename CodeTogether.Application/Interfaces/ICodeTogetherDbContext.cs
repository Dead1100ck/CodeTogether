using Microsoft.EntityFrameworkCore;

using CodeTogether.DTO;


namespace CodeTogether.Application.Interfaces
{
	public interface ICodeTogetherDbContext
	{
		DbSet<Room> Rooms { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
