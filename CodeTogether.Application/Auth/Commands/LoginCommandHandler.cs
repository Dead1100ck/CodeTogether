using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using CodeTogether.Application.Interfaces;
using CodeTogether.Application.Models.Requests.Auth.Request;
using CodeTogether.Application.Models.Responses.Auth;


namespace CodeTogether.Application.Auth.Commands
{
    public class LoginCommandHandler
	{
		private ICodeTogetherDbContext _dbcontext;
		private ITokenService _tokenService;

		public LoginCommandHandler(ICodeTogetherDbContext dbContext, ITokenService tokenService) => (_dbcontext, _tokenService) = (dbContext, tokenService);


		public async Task<GetTokensResponseModel> HandleAsync(LoginUserModel loginUserModel)
		{
			var findUser = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Username == loginUserModel.Username && u.Password == loginUserModel.Password);

			if (findUser == null)
				throw new Exception("BadRequest");
			
			List<Claim> Claims = [
				new Claim("Name", findUser.Name),
				new Claim("Surname", findUser.Surname),
				new Claim("Username", findUser.Username),
				new Claim("Email", findUser.Email)
			];

			findUser.AccessToken = _tokenService.GetAccessToken(Claims, out DateTime expires);
			findUser.TokenExpires = expires;
			findUser.RefreshToken = _tokenService.GetRefreshToken();

			await _dbcontext.SaveChangesAsync(CancellationToken.None);
			
			return new GetTokensResponseModel().MappingUserToGetTokensResponseModel(findUser);
		}
	}
}
