using System.Text.Json.Serialization;

using CodeTogether.DB;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCodeTogetherDbContext(builder.Configuration);
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
