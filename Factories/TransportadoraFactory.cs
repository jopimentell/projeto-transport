using FastLogistics.Models;

namespace FastLogistics.Factories
{
    public abstract class TransportadoraFactory
    {
        public abstract ITransportadora CriarTransportadora();
        
        public (double frete, int prazo) CalcularFreteEPrazo(double peso, double distancia)
        {
            var transportadora = CriarTransportadora();
            double frete = transportadora.CalcularFrete(peso, distancia);
            int prazo = transportadora.GetPrazo();
            return (frete, prazo);
        }
    }
}