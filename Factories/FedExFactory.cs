using FastLogistics.Models;

namespace FastLogistics.Factories
{
    public class FedExFactory : TransportadoraFactory
    {
        public override ITransportadora CriarTransportadora()
        {
            return new FedEx();
        }
    }
}