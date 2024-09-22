using EcommerceBlazor.Data;

namespace EcommerceBlazor.Utility;

public static class SD
{
    public static string Role_Admin = "Admin";
    public static string Role_Customer = "Customer";

    public static string StatusPending = "Pending";
    public static string StatusReadyForPickup = "ReadyForPickup";
    public static string StatusCompleted = "Completed";
    public static string StatusCancelled = "Cancelled";

    public static List<OrderDetail> ConvertShoppingCartListToOrderDetail(List<ShoppingCart> shoppingCarts)
    {
        List<OrderDetail> orderDetails = new();
        foreach (var cart in shoppingCarts)
        {
            OrderDetail orderDetail = new()
            {
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                Price = Convert.ToDouble(cart.Product.Price),
                ProductName = cart.Product.Name
            };
            orderDetails.Add(orderDetail);
        }

        return orderDetails;

    }
}
