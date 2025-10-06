using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryDemo
{
    internal sealed class CartItem
    {
        public int ProduktID { get; set; }
        public string Name { get; set; } = "";
        public decimal Preis { get; set; }
        public int Menge { get; set; }
        public decimal Sum => Preis * Menge;
    }
}
