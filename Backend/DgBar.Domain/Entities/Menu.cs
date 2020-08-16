using System;
using System.Collections.Generic;
using System.Text;

namespace DgBar.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public decimal? Price { get; set; }
    }
}
