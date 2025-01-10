namespace CodeTogether.DB
{
	public class DbInitializer
	{
		public static void Initialize(CodeTogetherDbContext context)
		{
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
		}
	}
}
