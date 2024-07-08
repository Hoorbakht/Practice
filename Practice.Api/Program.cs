using Microsoft.EntityFrameworkCore;
using Practice.Business;
using Practice.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
	.Services
	.AddHealthChecks()
	.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddScoped<IPersonBusiness, PersonBusiness>()
	.AddDbContextPool<PracticeContext>(option => option.UseInMemoryDatabase("Practice"), 2048);

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();

await using var context = scope.ServiceProvider.GetRequiredService<PracticeContext>();

await context.Database.EnsureCreatedAsync();

if (app.Environment.IsDevelopment())
	app.UseSwagger()
		.UseSwaggerUI();

app.UseHttpsRedirection()
	.UseHsts();

app.MapControllers();
app.MapHealthChecks("HealthChecks");

await app.RunAsync();


public partial class Program
{
}