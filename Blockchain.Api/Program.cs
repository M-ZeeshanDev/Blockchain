using Blockchain.Api;
using Blockchain.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// AddCors
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:7139");
                          policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

//DB Connection
string connectionString = builder.Configuration.GetConnectionString("BlockchainDbString");
builder.Services.AddDbContext<BlockchainDbContext>(options => {
    options.UseSqlServer(connectionString,
    sqlServerOpt => {
        sqlServerOpt.MigrationsAssembly("Blockchain.Persistence.Migrations");
    });
});

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blockchain Web API's", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});

builder.Services.RegisterServices(builder.Configuration, builder.Environment);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blockchain Web API's v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

StartUpClass startUpClass = new StartUpClass();
startUpClass.SeedDatabase(app);

app.Run();

public class StartUpClass
{
    public void SeedDatabase(IApplicationBuilder app)
    {
        using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            BlockchainDbContext context = serviceScope.ServiceProvider.GetService<BlockchainDbContext>();
            context.EnsureSeedData().GetAwaiter().GetResult();
        }
    }
}

