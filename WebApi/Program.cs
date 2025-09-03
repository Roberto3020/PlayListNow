using BLL.Abstract;
using BLL.Implements;
using DAL.Abstract;
using DAL.Implements;
using DAL.Context;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ISongRepository, SongRepository>();
builder.Services.AddSingleton<ISongService, SongService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
       .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
       .AddCheck("Memory", () =>
       {
           var memory = GC.GetTotalMemory(false) / (1024 * 1024);
           return memory < 500 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy("High memory usage");
       });

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .SetIsOriginAllowed(_ => true) // Permite cualquier origen (incluye cualquier puerto)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseRouting();
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        var response = new WebApi.Responses.ApiResponse<string>
        {
            Success = false,
            Message = "Ha ocurrido un error inesperado.",
            Errors = new[] { "Error interno del servidor." }
        };
        await context.Response.WriteAsJsonAsync(response);
    });
});

app.Run();
