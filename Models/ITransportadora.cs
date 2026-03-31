namespace FastLogistics.Models
{
    public interface ITransportadora
    {
        double CalcularFrete(double peso, double distancia);
        string GetNome();
        int GetPrazo();
    }
}