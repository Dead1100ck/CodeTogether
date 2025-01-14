namespace CodeTogether.DTO
{
	public class User
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTime TokenExpires { get; set; }
	}
}
