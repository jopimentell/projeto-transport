namespace FastLogistics.Models
{
    public class Correios : ITransportadora
    {
        public double CalcularFrete(double peso, double distancia)
        {
            return (peso * 2.0) + (distancia * 0.5);
        }
        
        public string GetNome() => "Correios";
        public int GetPrazo() => 5;
    }
}