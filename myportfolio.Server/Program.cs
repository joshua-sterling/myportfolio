using Microsoft.EntityFrameworkCore;
using myportfolio.Server.DataAccess;
using NLog.Web;
using NLog;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddEnvironmentVariables();
    string connectionString;
    if (builder.Environment.IsDevelopment())
    {
        connectionString = builder.Configuration["DevelopmentConnection"];
    }
    else
    {
        connectionString = builder.Configuration["DefaultConnection"];
    }
    builder.Services.AddDbContext<MyPortfolioContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseDefaultFiles();
    app.UseStaticFiles();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapFallbackToFile("/index.html");

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}