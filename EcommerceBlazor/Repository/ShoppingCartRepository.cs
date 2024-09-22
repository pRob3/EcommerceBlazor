using EcommerceBlazor.Data;
using EcommerceBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBlazor.Repository;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ApplicationDbContext _db;
    public ShoppingCartRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ClearCartAsync(string? userId)
    {
        var cartItems = _db.ShoppingCarts.Where(x => x.UserId == userId);
        _db.ShoppingCarts.RemoveRange(cartItems);

        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
    {
        return await _db.ShoppingCarts.Where(x => x.UserId == userId)
            .Include(p => p.Product)
            .ToListAsync();
    }

    public async Task<int> GetTotalCartCountAsync(string? userId)
    {
        int cartCount = 0;
        var cartItems = await _db.ShoppingCarts.Where(x => x.UserId == userId).ToListAsync();
        if (cartItems != null)
        {
            cartCount = cartItems.Sum(x => x.Quantity);
        }

        return cartCount;
    }

    public async Task<bool> UpdateCartAsync(string userId, int productId, int quantityUpdate)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return false;
        }

        var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

        if (cart is null)
        {
            cart = new ShoppingCart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantityUpdate
            };
            await _db.ShoppingCarts.AddAsync(cart);
        }
        else
        {
            cart.Quantity += quantityUpdate;
            if (cart.Quantity <= 0)
            {
                _db.ShoppingCarts.Remove(cart);
            }
        }

        return await _db.SaveChangesAsync() > 0;
    }
}
