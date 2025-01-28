using CodeTogether.DTO;


namespace CodeTogether.Application.Models.Responses.Auth
{
	public class GetTokensResponseModel
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTime TokenExpires { get; set; }

		public GetTokensResponseModel MappingUserToGetTokensResponseModel (User user)
		{
			return new GetTokensResponseModel
			{
				AccessToken = user.AccessToken,
				RefreshToken = user.RefreshToken,
				TokenExpires = user.TokenExpires,
			};
		}
	}
}
