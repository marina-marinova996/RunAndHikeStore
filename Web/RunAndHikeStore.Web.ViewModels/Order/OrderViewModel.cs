﻿using RunAndHikeStore.Web.ViewModels.ShoppingCart;

namespace RunAndHikeStore.Web.ViewModels.Order
{
    using RunAndHikeStore.Data.Models.Enums;
    using System;
    using System.Collections.Generic;

    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal OrderTotalPrice { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public string CustomerId { get; set; }

        public IEnumerable<OrderDetailFormViewModel> OrderDetails { get; set; }

        public IEnumerable<CartItemViewModel> CartItems { get; set; }
    }
}