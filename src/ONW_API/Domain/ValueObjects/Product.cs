using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Domain.ValueObjects
{
    public sealed class Product
    {
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal Weight { get; private set; }

        protected Product() { }

        public Product(string name, int quantity, decimal weight)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome do produto obrigatório");
            if (quantity <= 0) throw new ArgumentException("Quantidade deve ser maior que zero");
            if (weight <= 0) throw new ArgumentException("Peso deve ser maior que zero");

            Name = name;
            Quantity = quantity;
            Weight = weight;
        }
    }
}