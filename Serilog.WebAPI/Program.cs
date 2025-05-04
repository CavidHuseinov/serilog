using Microsoft.EntityFrameworkCore;
using Serilog.WebAPI.Context;

namespace Serilog.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SerilogDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Deploy"));
            });


            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
