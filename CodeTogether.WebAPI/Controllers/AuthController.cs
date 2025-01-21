using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
			var command = new LoginCommandHandler(_dbContext, tokenService);
			var response = await command.HandleAsync(loginUserModel);

			Response.Cookies.Append("AccessToken", response.AccessToken);

			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<User>> Refresh([FromBody] RefreshTokenModel refreshTokenModel, [FromServices] ITokenService tokenService)
		{
			var command = new RefreshCommandHandler(_dbContext, tokenService);
			var response = await command.HandleAsync(refreshTokenModel);

			Response.Cookies.Append("AccessToken", response.AccessToken);

			return Ok(response);
		}

		[HttpGet]
		[Authorize]
		public ActionResult AuthCheck()
		{
			return Ok();
		}
	}
}
