using EcommerceBlazor.Data;
using EcommerceBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBlazor.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;

    public OrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
    {
        orderHeader.OrderDate = DateTime.Now;
        await _db.OrderHeaders.AddAsync(orderHeader);
        await _db.SaveChangesAsync();
        return orderHeader;

    }

    public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            return await _db.OrderHeaders.Where(x => x.UserId == userId).ToListAsync();
        }

        return await _db.OrderHeaders.ToListAsync();
    }

    public async Task<OrderHeader> GetAsync(int id)
    {
        return await _db.OrderHeaders.Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateStatusAsync(int orderId, string status)
    {
        var orderHeader = await _db.OrderHeaders.FirstOrDefaultAsync(x => x.Id == orderId);
        if (orderHeader == null)
        {
            return false;
        }

        orderHeader.Status = status;
        return (await _db.SaveChangesAsync() > 0);

    }
}
