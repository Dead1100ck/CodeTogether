using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using CodeTogether.Application.Interfaces;


namespace CodeTogether.Auth
{
	public class TokenService : ITokenService
	{
		private readonly IAuthOptions _option;

		public TokenService(IAuthOptions option)
		{
			_option = option;
		}

		public string GetAccessToken(IEnumerable<Claim> claims, out DateTime expires)
		{
			expires = DateTime.UtcNow.AddSeconds(_option.TokenLifetime);
			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.SecretKey));
			SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken token;

			if (claims.Count() == 4)
			{
				token = new JwtSecurityToken(
					issuer: _option.Issuer,
					audience: _option.Audience,
					claims: claims,
					expires: expires,
					signingCredentials: signingCredentials
				);
			}
			else
			{
				token = new JwtSecurityToken(
					issuer: _option.Issuer,
					claims: claims,
					expires: expires,
					signingCredentials: signingCredentials
				);
			}

			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
			return handler.WriteToken(token);
		}

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.SecretKey));
			TokenValidationParameters validationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = _option.ValidateIssuer,
				ValidateAudience = _option.ValidateAudience,
				ValidateIssuerSigningKey = _option.ValidateIssuerSigningKey,
				ValidIssuer = _option.Issuer,
				ValidAudience = _option.Audience,
				IssuerSigningKey = key,
				ValidateLifetime = false
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principial = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
			var jwtSecurityToken = securityToken as JwtSecurityToken;
			
			if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Invalid token");
			
			return principial;
		}

		public string GetRefreshToken()
		{
			var randomNumber = new byte[32];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToHexString(randomNumber);
		}
	}
}
