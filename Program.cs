using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartInventoryBE;
using SmartInventoryBE.Hubs;
using SmartInventoryBE.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddIdentity(builder.Configuration.GetSection("JWT"));
builder.Services.AddRepositories();
builder.Services.AddServices();

// Add AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSignalR();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<GlobalExceptionHandler>();

// Add CORS services  --- ADD THIS SECTION ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // Allow your Next.js frontend origin
            .AllowAnyMethod() // Allow all HTTP methods (GET, POST, PUT, DELETE, etc.)
            .AllowAnyHeader() // Allow all headers
            .AllowCredentials(); // If you need to send cookies or authorization headers with SignalR
    });
});
// -


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartInventory app", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            new List < string > ()
        }
    });
});


//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Enable CORS --- ADD THIS LINE ---
app.UseCors("DevCorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.MapHub<InventoryHub>("/hubs/inventory");

app.Run();
