using EcommerceBlazor.Data;

namespace EcommerceBlazor.Repository.IRepository;

public interface IOrderRepository
{
    public Task<OrderHeader> CreateAsync(OrderHeader orderHeader);
    public Task<OrderHeader> GetAsync(int id);
    public Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null);
    public Task<bool> UpdateStatusAsync(int orderId, string status);
}
