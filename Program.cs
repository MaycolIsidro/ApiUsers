using API_Users.Services;
using API_Users.Settings;
using Microsoft.EntityFrameworkCore;

namespace API_Users
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("secrets.json")
                .AddEnvironmentVariables()
                .Build();

            // Add services to the container.
            builder.Services.Configure<MailSettings>(config.GetRequiredSection("MailSettings"));
            builder.Services.AddTransient<IMailService, MailService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<ApiContext>(opt =>
            {
                opt.UseInMemoryDatabase("Users");
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
