namespace CodeTogether.DB
{
	public class DbInitializer
	{
		public static void Initialize(CodeTogetherDbContext context)
		{
			context.Database.EnsureCreated();
		}
	}
}
