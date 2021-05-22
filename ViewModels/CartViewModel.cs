﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.ViewModels
{
    public class CartViewModel
    {
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
        public decimal ItemPrice { get; set; }

        public CartViewModel(string itemName, int itemCount, decimal itemPrice)
        {
            ItemName = itemName;
            ItemCount = itemCount;
            ItemPrice = itemPrice;
        }
    }
}
