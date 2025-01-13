using Microsoft.AspNetCore.Mvc;

using CodeTogether.DTO;
using CodeTogether.DB;
using CodeTogether.Application.Models.Auth;
using CodeTogether.Application.Auth.Commands;


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
		public async Task<ActionResult<User>> Login([FromBody] LoginUserModel loginUserModel)
		{
			var command = new LoginCommandHandler(_dbContext);
			var response = await command.HandleAsync(loginUserModel);

			return Ok(response);
		}
	}
}
