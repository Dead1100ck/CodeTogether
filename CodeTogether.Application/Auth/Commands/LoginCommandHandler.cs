using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using CodeTogether.Application.Interfaces;
using CodeTogether.Application.Models.Auth;
using CodeTogether.DTO;


namespace CodeTogether.Application.Auth.Commands
{
	public class LoginCommandHandler
	{
		private ICodeTogetherDbContext _dbcontext;
		private ITokenService _tokenService;

		public LoginCommandHandler(ICodeTogetherDbContext dbContext, ITokenService tokenService) => (_dbcontext, _tokenService) = (dbContext, tokenService);


		public async Task<User> HandleAsync(LoginUserModel loginUserModel, IEnumerable<Claim> claims)
		{
			var findUser = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Username == loginUserModel.Username && u.Password == loginUserModel.Password);

			if (findUser == null)
				throw new Exception("BadRequest");

			findUser.AccessToken = _tokenService.GetAccessToken(claims, out DateTime expires);
			findUser.TokenExpires = expires;
			findUser.RefreshToken = _tokenService.GetRefreshToken();

			await _dbcontext.SaveChangesAsync(CancellationToken.None);

			return findUser;
		}
	}
}
