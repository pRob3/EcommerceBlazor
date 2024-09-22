using EcommerceBlazor.Data;
using EcommerceBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace EcommerceBlazor.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly HybridCache _hybridCache;
    private const string CacheKey = "ProductList";

    public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, HybridCache hybridCache)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
        _hybridCache = hybridCache;
    }

    public async Task<Product> CreateAsync(Product obj)
    {
        await _db.Products.AddAsync(obj);
        await _db.SaveChangesAsync();

        // Delete cache
        await _hybridCache.RemoveAsync(CacheKey);

        return obj;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var obj = _db.Products.FirstOrDefault(x => x.Id == id);

        if (obj == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(obj.ImageUrl))
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        _db.Products.Remove(obj);

        // Delete cache
        await _hybridCache.RemoveAsync(CacheKey);
        await _hybridCache.RemoveAsync($"product:{obj.Id}");

        return (await _db.SaveChangesAsync() > 0);
    }

    public async Task<Product> GetAsync(int id)
    {
        var cacheKey = $"product:{id}";
        var product = await _hybridCache.GetOrCreateAsync(cacheKey, async _ => await GetProductAsync(id));

        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        //return await _db.Products.Include(c => c.Category).ToListAsync();


        var products = await _hybridCache.GetOrCreateAsync(CacheKey,
            async _ => /*await GetProductListAsync()*/ await _db.Products.Include(c => c.Category).ToListAsync());

        return products;
    }

    public async Task<Product> UpdateAsync(Product obj)
    {
        var objFromDb = await _db.Products.FirstOrDefaultAsync(x => x.Id == obj.Id);
        if (objFromDb is null)
        {
            return new Product();
        }

        objFromDb.Name = obj.Name;
        objFromDb.Description = obj.Description;
        objFromDb.ImageUrl = obj.ImageUrl;
        objFromDb.CategoryId = obj.CategoryId;
        objFromDb.Price = obj.Price;

        await _db.SaveChangesAsync();

        // Delete cache
        await _hybridCache.RemoveAsync(CacheKey);
        await _hybridCache.RemoveAsync($"product:{obj.Id}");

        return objFromDb;
    }

    private async Task<Product> GetProductAsync(int id)
    {
        var obj = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (obj == null)
        {
            return new Product();
        }

        return obj;
    }

    private async Task<IEnumerable<Product>> GetProductListAsync()
    {
        return await _db.Products.Include(c => c.Category).ToListAsync();
    }
}
