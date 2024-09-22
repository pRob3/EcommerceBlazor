using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBlazor.Data;

public class ShoppingCart
{
    public int Id { get; set; }

    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }

    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    public int Quantity { get; set; }
}
