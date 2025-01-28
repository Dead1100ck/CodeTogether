using CodeTogether.Application.Interfaces;
using CodeTogether.Application.Models.Requests.Auth.Create;
using CodeTogether.DTO;


namespace CodeTogether.Application.Auth.Commands
{
    public class RegisterCommandHandler
	{
		private ICodeTogetherDbContext _dbContext;

		public RegisterCommandHandler (ICodeTogetherDbContext dbContext) => _dbContext = dbContext;


		public async Task HandleAsync(RegisterUserModel registerUserModel)
		{
			User user = new User() { 
				Username = registerUserModel.Username,
				Name = registerUserModel.Name,
				Surname = registerUserModel.Surname,
				Email = registerUserModel.Email,
				Password = registerUserModel.Password,
			};

			await _dbContext.Users.AddAsync(user);
			await _dbContext.SaveChangesAsync(CancellationToken.None);

			return;
		}
	}
}
