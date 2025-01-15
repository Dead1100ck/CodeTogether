using CodeTogether.Application.Interfaces;


namespace CodeTogether.WebAPI.Properties
{
	public class AuthOptions : IAuthOptions
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string SecretKey { get; set; }
		public bool ValidateAudience { get; set; }
		public bool ValidateIssuer { get; set; }
		public bool ValidateLifetime { get; set; }
		public bool ValidateIssuerSigningKey { get; set; }
		public int TokenLifetime { get; set; }
	}
}
