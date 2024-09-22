using EcommerceBlazor.Data;
using EcommerceBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBlazor.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Category> CreateAsync(Category obj)
    {
        await _db.Categories.AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var obj = _db.Categories.FirstOrDefault(x => x.Id == id);
        if (obj == null)
        {
            return false;
        }

        _db.Categories.Remove(obj);
        return (await _db.SaveChangesAsync() > 0);
    }

    public async Task<Category> GetAsync(int id)
    {
        var obj = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (obj == null)
        {
            return new Category();
        }

        return obj;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _db.Categories.ToListAsync();
    }

    public async Task<Category> UpdateAsync(Category obj)
    {
        var objFromDb = await _db.Categories.FirstOrDefaultAsync(x => x.Id == obj.Id);
        if (objFromDb is null)
        {
            return new Category();
        }

        objFromDb.Name = obj.Name;
        await _db.SaveChangesAsync();
        return objFromDb;
    }
}
