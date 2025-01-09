using Microsoft.EntityFrameworkCore;

using CodeTogether.DTO;
using CodeTogether.DB.EntityTypeConfigurations;


namespace CodeTogether.DB
{
	public class CodeTogetherDbContext: DbContext
	{
		public DbSet<Room> Rooms { get; set; }


		public CodeTogetherDbContext(DbContextOptions<CodeTogetherDbContext> options): base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new RoomConfiguration());
			base.OnModelCreating(builder);
		}
	}
}
