namespace Persistence
{
	public class DbInitializer : IDbInitializer
	{
		private readonly StoreContext _storeContext;

		public DbInitializer(StoreContext storeContext)
		{
			_storeContext = storeContext;
		}

		public async Task InitializeAsync()
		{
			
			try
			{
				// create database if it does not exist & applying any pending migrations
				if (_storeContext.Database.GetPendingMigrations().Any())
				{
					await _storeContext.Database.MigrateAsync();
				}


				// Data seeding

				// product type
				if (!_storeContext.ProductTypes.Any())
				{
					//read types from file as string
					var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

					// transform into C# objects
					var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

					// add to db & save
					if (types is not null && types.Any())
					{
						await _storeContext.ProductTypes.AddRangeAsync(types);
						await _storeContext.SaveChangesAsync();
					}
				}

				// product brand

				if (!_storeContext.ProductBrands.Any())
				{
					//read brands from file as string
					var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

					// transform into C# objects
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

					// add to db & save
					if (brands is not null && brands.Any())
					{
						await _storeContext.ProductBrands.AddRangeAsync(brands);
						await _storeContext.SaveChangesAsync();
					}
				}

				// products
				if (!_storeContext.Products.Any())
				{
					//read products from file as string
					var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

					// transform into C# objects
					var products = JsonSerializer.Deserialize<List<Product>>(productsData);

					// add to db & save
					if (products is not null && products.Any())
					{
						await _storeContext.Products.AddRangeAsync(products);
						await _storeContext.SaveChangesAsync();
					}
				}
			}
			catch (Exception ex)
			{

			}
		}
	}
}
