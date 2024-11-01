using Domain.Contracts;
using Persistence;

namespace Ecommerce
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddScoped<IDbInitializer,DbInitializer>();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});


			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			await InitializeDbAsync(app);





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


			async Task InitializeDbAsync(WebApplication app)
			{
				// Create object from type that implements IDbInitializer
				using var scope = app.Services.CreateScope();
				var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

				await dbInitializer.InitializeAsync();

			}


		}

	}
}
