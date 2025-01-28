using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using CodeTogether.DB;
using CodeTogether.Application.Auth.Commands;
using CodeTogether.Application.Interfaces;
using CodeTogether.Application.Models.Requests.Auth.Create;
using CodeTogether.Application.Models.Requests.Auth.Request;
using CodeTogether.Application.Models.Requests.Auth.Update;
using CodeTogether.Application.Models.Responses.Auth;


namespace CodeTogether.WebAPI.Controllers
{
    public class AuthController : BaseController
	{
		public AuthController(CodeTogetherDbContext dbContext) : base(dbContext) { }


		[HttpPost]
		public async Task<ActionResult> Register([FromBody] RegisterUserModel registerUserModel)
		{
			var command = new RegisterCommandHandler(_dbContext);
			await command.HandleAsync(registerUserModel);

			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult<GetTokensResponseModel>> Login([FromBody] LoginUserModel loginUserModel, [FromServices] ITokenService tokenService)
		{
			var command = new LoginCommandHandler(_dbContext, tokenService);
			var response = await command.HandleAsync(loginUserModel);

			Response.Cookies.Append("AccessToken", response.AccessToken);

			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<GetTokensResponseModel>> Refresh([FromBody] RefreshTokenModel refreshTokenModel, [FromServices] ITokenService tokenService)
		{
			var command = new RefreshCommandHandler(_dbContext, tokenService);
			var response = await command.HandleAsync(refreshTokenModel);

			Response.Cookies.Append("AccessToken", response.AccessToken);

			return Ok(response);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Check()
		{
			return Ok("You are is authorize");
		}
	}
}
