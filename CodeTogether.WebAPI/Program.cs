using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using CodeTogether.DB;
using CodeTogether.Auth;
using CodeTogether.WebAPI.Properties;


var builder = WebApplication.CreateBuilder(args);
var authOptions = builder.Configuration.GetSection("Jwt").Get<AuthOptions>();
Console.WriteLine(authOptions);

builder.Services.AddCodeTogetherDbContext(builder.Configuration);
builder.Services.AddTokenService(authOptions);
builder.Services.AddAuthentication(auth =>
{
	auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
	options.RequireHttpsMetadata = true;
	options.SaveToken = true;
	options.IncludeErrorDetails = true;

	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = authOptions.ValidateIssuer,
		ValidateAudience = authOptions.ValidateAudience,
		ValidateIssuerSigningKey = authOptions.ValidateIssuerSigningKey,
		ValidIssuer = authOptions.Issuer,
		ValidAudience = authOptions.Audience,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecretKey)),
		ClockSkew = TimeSpan.Zero,
	};
});
/*builder.Services.AddAuthorization(options =>
{

});*/
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(config =>
{
	config.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyHeader();
		policy.AllowAnyMethod();
		policy.AllowAnyOrigin();
	});
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseAuthentication();
using (var scope = app.Services.CreateScope())
{
	var serviceProvider = scope.ServiceProvider;
	try
	{
		var context = serviceProvider.GetRequiredService<CodeTogetherDbContext>();
		DbInitializer.Initialize(context);
	}
	catch (Exception exeption)
	{

	}
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
