using CodeTogether.Application.Interfaces;
using CodeTogether.DTO;
using Microsoft.EntityFrameworkCore;


namespace CodeTogether.Application.Auth.Commands
{
	public class RefreshCommandHandler
	{
		private ICodeTogetherDbContext _dbContext;
		private ITokenService _tokenService;

		public RefreshCommandHandler(ICodeTogetherDbContext dbContext, ITokenService tokenService) => (_dbContext, _tokenService) = (dbContext, tokenService);


		public async Task<User> HandleAsync(ITokenService tokenService, string refreshToken)
		{
			User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

			if (user == null)
				throw new Exception("Token invalid");

			var principial = tokenService.GetPrincipalFromExpiredToken(user.AccessToken);
			string accessToken = tokenService.GetAccessToken(principial.Claims, out DateTime expires);

			user.AccessToken = accessToken;
			user.RefreshToken = refreshToken;
			await _dbContext.SaveChangesAsync(CancellationToken.None);

			return user;
		}
	}
}
