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

    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddDbContext<MyPortfolioContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

    builder.Services.AddScoped<RowingEventRepository>();
    

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    //no standard JSON representation for TimeSpan and DateOnly, so here we tell Swagger how to represent them
    //makes the Swagger UI a little friendlier
    builder.Services.AddSwaggerGen(x =>
    {
        x.MapType<TimeSpan>(() => new Microsoft.OpenApi.Models.OpenApiSchema
        {
            Type = "string",
            Example = new Microsoft.OpenApi.Any.OpenApiString("00:02:00")
        });

        x.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
        {
            Type = "string",
            Format = "date",
            Example = new Microsoft.OpenApi.Any.OpenApiString("2024-03-01")
        });
    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowMyOrigin",
            builder => builder.WithOrigins("https://localhost:4200", "https://joshsterling.net", "http://jsterling-001-site3.htempurl.com")
                               .AllowAnyHeader()
                               .AllowAnyMethod());
    });

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    var app = builder.Build();
    app.UseCors("AllowMyOrigin");
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