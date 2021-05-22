using Microsoft.AspNetCore.Http;
using MyShop.Models;
using MyShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Extensions
{
    public class CartHelper
    {
        public List<ProductModel> CartProducts { get; set; }
        public CartHelper()
        {
            CartProducts = new List<ProductModel>();
        }

        public decimal GetProductsTotalPrice(List<ProductModel> products)
        {
            decimal price = 0;
            foreach (var item in products)
            {
                price += item.Price;
            }
            return price;
        }

        public Comparer<ProductModel> GetComparerByTitle()
        {
            var byTitle = Comparer<ProductModel>.Create((a, b) => a.Title.CompareTo(b.Title));
            return byTitle;
        }

        public List<CartViewModel> GroupedProducts(List<ProductModel> list)
        {
            var query = list.GroupBy(product => product.Title).Select(p => new
            {
                ProductName = p.Key,
                ProductCount = p.Count(),
                ProductPrice = p.Select(p => p.Price).Sum()
            }).OrderBy(p => p.ProductName);

            List<CartViewModel> cartProducts = new List<CartViewModel>();
            foreach (var item in query)
            {
                CartViewModel product = new CartViewModel(item.ProductName, item.ProductCount, item.ProductPrice);
                cartProducts.Add(product);
            }
            return cartProducts;
        }
    }
}
