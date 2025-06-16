using Identity_API.Model.Interfaces;
using Identity_API.Services;
using Identity_Service.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Identity_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                               .ReadFrom.Configuration(builder.Configuration)
                               .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "Entity API",
                    Version = "v1",
                    Description = "API for Entity WebApplication",
                });
            });

            // Add services to the container.
            builder.Services.AddDbContext<IdentityContext>(options =>
                 options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddControllers();

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IRequestService, RequestService>();

            builder.Services.AddScoped<IRoleService, RoleService>();

            builder.Services.AddScoped<IUserRoleService, UserRoleService>();

            var app = builder.Build();

            app.UseSwagger(c =>
            {
                c.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v3/swagger.json", "Entity API V1");
            });

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
