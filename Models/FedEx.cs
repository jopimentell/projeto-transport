namespace FastLogistics.Models
{
    public class FedEx : ITransportadora
    {
        public double CalcularFrete(double peso, double distancia)
        {
            return (peso * 3.0) + (distancia * 0.8);
        }
        
        public string GetNome() => "FedEx";
        public int GetPrazo() => 3;
    }
}