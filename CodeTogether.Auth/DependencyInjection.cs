using Microsoft.Extensions.DependencyInjection;

using CodeTogether.Auth.Interfaces;


namespace CodeTogether.Auth
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddTokenService(this IServiceCollection services, IAuthOptions options)
		{
			services.AddTransient<ITokenService>(t => new TokenService(options));

			return services;
		}
	}
}
