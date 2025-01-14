using System.Security.Claims;


namespace CodeTogether.Auth.Interfaces
{
	public interface ITokenService
	{
		public string GetAccessToken(IEnumerable<Claim> claims, out DateTime expires);
		public string GetRefreshToken();
		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	}
}
