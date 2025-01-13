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


		public async Task<User> HandleAsync(LoginUserModel loginUserModel)
		{
			var findUser = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Username == loginUserModel.Username && u.Password == loginUserModel.Password);

			if (findUser == null)
				throw new Exception("This user is not found");

			return findUser;
		}
	}
}
