using System;

namespace FastLogistics.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public double Peso { get; set; }
        public double Distancia { get; set; }
        public string Transportadora { get; set; }
        public double ValorFrete { get; set; }
        public DateTime Data { get; set; }
        
        public override string ToString()
        {
            return $"ID: {Id} | {Cliente} | {Transportadora} | R$ {ValorFrete:F2} | {Data:dd/MM/yyyy}";
        }
    }
}