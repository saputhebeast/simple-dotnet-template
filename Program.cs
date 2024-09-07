using UrbanNest.Middlewares;
using UrbanNest.Repositories;
using UrbanNest.Services;
using UrbanNest.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Configure Authentication and JWT Bearer token using AuthMiddleware
var authMiddleware = new AuthMiddleware(builder.Services, builder.Configuration);
authMiddleware.ConfigureJwtBearer();

// Configure MongoDB using MongoMiddleware
var mongoMiddleware = new MongoDbMiddleware(builder.Services, builder.Configuration);
mongoMiddleware.ConfigureMongoDb();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Service class registration
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Repository registration
builder.Services.AddScoped<UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
