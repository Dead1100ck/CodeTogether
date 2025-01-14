using Microsoft.EntityFrameworkCore;

using CodeTogether.Application.Interfaces;
using CodeTogether.Application.Models.Auth;
using CodeTogether.DTO;


namespace CodeTogether.Application.Auth.Commands
{
	public class LoginCommandHandler
	{
		private ICodeTogetherDbContext _dbcontext;
		
		public LoginCommandHandler(ICodeTogetherDbContext dbContext) => _dbcontext = dbContext;


		public async Task<User> HandleAsync(LoginUserModel loginUserModel, DateTime expires, string accessToken, string? refreshToken=null)
		{
			var findUser = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Username == loginUserModel.Username && u.Password == loginUserModel.Password);

			if (findUser == null)
				throw new Exception("BadRequest");

			findUser.AccessToken = accessToken;
			findUser.TokenExpires = expires;
			if (refreshToken != null)
				findUser.RefreshToken = refreshToken;

			await _dbcontext.SaveChangesAsync(CancellationToken.None);

			return findUser;
		}
	}
}
