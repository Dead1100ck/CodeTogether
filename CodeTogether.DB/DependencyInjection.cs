using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CodeTogether.DB
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCodeTogetherDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration["connectionString"];

			services.AddDbContext<CodeTogetherDbContext>(option =>
			{
				option.UseNpgsql(connectionString);
			});
			
			return services;
		}
	}
}
