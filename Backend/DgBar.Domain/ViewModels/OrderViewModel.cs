using DgBar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DgBar.Domain.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            Items = new List<Menu>();
     
            
        }

        public int Id { get; set; }
        public int IdItem { get; set; }
        public int Amount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? FinalPrice { get; set; }
        public List<Menu> Items { get; set; }
    }
}
