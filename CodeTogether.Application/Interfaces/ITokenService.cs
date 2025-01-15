using System.Security.Claims;


namespace CodeTogether.Application.Interfaces
{
	public interface ITokenService
	{
		public string GetAccessToken(out DateTime expires);
		public string GetAccessToken(IEnumerable<Claim> claims, out DateTime expires);
		public string GetRefreshToken();
		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	}
}
