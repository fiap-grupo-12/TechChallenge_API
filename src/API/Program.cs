using FIAP.TechChallenge.ByteMeBurguer.API.Extensions;
using FIAP.TechChallenge.ByteMeBurguer.Application;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = SecretsService.GetSecret("sql_connection_string");

builder.Services.AddProjectDependencies(connectionString);

//HealthCheck
builder.Services.AddHealthChecks().AddSqlServer(connectionString!);
builder.Services.AddHealthChecks().AddCheck("application", () =>
{
    return HealthCheckResult.Healthy("Aplicação em execução");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Tech Challenge");
});

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    initializer.Initialize();
}

app.UseHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

