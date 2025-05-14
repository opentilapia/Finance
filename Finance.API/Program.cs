using Finance.API.DataService;
using Finance.API.DataService.Interface;
using Finance.API.Service;
using Finance.API.Service.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Finance.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IEntryService, EntrySevice>();
            builder.Services.AddScoped<IEntryRepository, EntryRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();

            // Configure MongoDBSettings from appsettings.json
            builder.Services.Configure<MongoDBSettings>(
                builder.Configuration.GetSection("MongoDBSettings"));

            // Register MongoDB client and database to DI container
            builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            builder.Services.AddSingleton(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });

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
