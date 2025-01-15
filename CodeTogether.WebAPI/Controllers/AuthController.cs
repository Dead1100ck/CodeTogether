using Microsoft.AspNetCore.Mvc;

using CodeTogether.DTO;
using CodeTogether.DB;
using CodeTogether.Application.Models.Auth;
using CodeTogether.Application.Auth.Commands;
using CodeTogether.Application.Interfaces;


namespace CodeTogether.WebAPI.Controllers
{
	public class AuthController : BaseController
	{
		public AuthController(CodeTogetherDbContext dbContext) : base(dbContext) { }


		[HttpPost]
		public async Task<ActionResult<User>> RegisterUser([FromBody] RegisterUserModel registerUserModel)
		{
			var command = new RegisterCommandHandler(_dbContext);
			var response = await command.HandleAsync(registerUserModel);

			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<User>> Login([FromBody] LoginUserModel loginUserModel, [FromServices] ITokenService tokenService)
		{
			var command = new LoginCommandHandler(_dbContext);
			string accessToken = tokenService.GetAccessToken(out DateTime expires);
			string refreshToken = tokenService.GetRefreshToken();
			var response = await command.HandleAsync(loginUserModel, expires, accessToken, refreshToken);

			Response.Cookies.Append("AccessToken", accessToken);

			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<User>> Refresh([FromBody] LoginUserModel loginUserModel, [FromServices] ITokenService tokenService)
		{
			var command = new LoginCommandHandler(_dbContext);
			string accessToken = tokenService.GetAccessToken(out DateTime expires);
			string refreshToken = tokenService.GetRefreshToken();
			var response = await command.HandleAsync(loginUserModel, expires, accessToken, refreshToken);

			//var principial = tokenService.GetPrincipalFromExpiredToken(accessToken);
			//string accessToken = tokenService.GetAccessToken((IEnumerable<System.Security.Claims.Claim>)principial, out DateTime expires);

			Response.Cookies.Append("AccessToken", accessToken);

			return Ok(response);
		}
	}
}
