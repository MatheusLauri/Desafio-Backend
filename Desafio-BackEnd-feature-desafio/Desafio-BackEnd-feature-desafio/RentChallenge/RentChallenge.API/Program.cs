using RentChallenge.API;
using RentChallenge.API.Middelwares;
using RentChallenge.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

Configuration.ResolveDepedencies(builder);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

await SeedData.ApplySeedWithRetryAsync(app.Services);

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
