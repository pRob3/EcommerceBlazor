﻿using System.ComponentModel.DataAnnotations;

namespace EcommerceBlazor.Data;

public class OrderDetail
{
    public int Id { get; set; }
    public int OrderHeaderId { get; set; }
    public OrderHeader OrderHeader { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public double Price { get; set; }
    [Required]
    public string ProductName { get; set; }
}
