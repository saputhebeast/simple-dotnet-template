using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;

namespace UrbanNest.Middlewares;

public class AuthMiddleware
{
    private readonly IServiceCollection _serviceCollection;
        
    private readonly IConfiguration _configuration;

    public AuthMiddleware(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        _serviceCollection = serviceCollection;
        _configuration = configuration;
    }

    public void ConfigureJwtBearer()
    {
        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);

        _serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        // If the token is missing, return a custom message
                        if (string.IsNullOrEmpty(context.Request.Headers.Authorization))
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            return context.Response.WriteAsync("{\"error\": \"Authorization header is missing. Please provide a valid Bearer token.\"}");
                        }

                        // Handle invalid token case (for example, token is present but invalid)
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"error\": \"You are not authorized to access this resource. Invalid or missing token.\"}");
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"error\": \"You do not have access to this resource.\"}");
                    }
                };
            });
        
        // Configure Swagger to handle JWT Bearer token
        _serviceCollection.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = """Standard Authorization header using the Bearer scheme. Example: "Bearer {token}" """,
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
            });
            c.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }
}
