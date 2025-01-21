using CodeTogether.Application.Interfaces;
using CodeTogether.Application.Models.Auth;
using CodeTogether.DTO;
using Microsoft.EntityFrameworkCore;


namespace CodeTogether.Application.Auth.Commands
{
	public class RefreshCommandHandler
	{
		private ICodeTogetherDbContext _dbContext;
		private ITokenService _tokenService;

		public RefreshCommandHandler(ICodeTogetherDbContext dbContext, ITokenService tokenService) => (_dbContext, _tokenService) = (dbContext, tokenService);


		public async Task<User> HandleAsync(RefreshTokenModel refreshTokenModel)
		{
			string refreshToken = refreshTokenModel.RefreshToken;

			User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

			if (user == null)
				throw new Exception("Token invalid");

			var principial = _tokenService.GetPrincipalFromExpiredToken(user.AccessToken);
			string accessToken = _tokenService.GetAccessToken(principial.Claims, out DateTime expires);

			user.TokenExpires = expires;
			user.AccessToken = accessToken;
			user.RefreshToken = refreshToken;
			await _dbContext.SaveChangesAsync(CancellationToken.None);

			return user;
		}
	}
}
