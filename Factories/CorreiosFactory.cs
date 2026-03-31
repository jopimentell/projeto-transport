using FastLogistics.Models;

namespace FastLogistics.Factories
{
    public class CorreiosFactory : TransportadoraFactory
    {
        public override ITransportadora CriarTransportadora()
        {
            return new Correios();
        }
    }
}