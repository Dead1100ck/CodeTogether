namespace CodeTogether.Application.Models.Auth
{
	public class LoginUserModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string? RefreshToken { get; set; }
	}
}
