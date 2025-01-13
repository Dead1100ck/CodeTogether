using Microsoft.EntityFrameworkCore;

using CodeTogether.DTO;
using CodeTogether.DB.EntityTypeConfigurations;
using CodeTogether.Application.Interfaces;


namespace CodeTogether.DB
{
	public class CodeTogetherDbContext: DbContext, ICodeTogetherDbContext
	{
		public DbSet<Room> Rooms { get; set; }
		public DbSet<User> Users { get; set; }


		public CodeTogetherDbContext(DbContextOptions<CodeTogetherDbContext> options): base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new RoomConfiguration());
			builder.ApplyConfiguration(new UserConfiguration());

			base.OnModelCreating(builder);
		}
	}
}
