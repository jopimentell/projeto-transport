using System;
using FastLogistics.Models;
using FastLogistics.Factories;
using FastLogistics.Repositories;

namespace FastLogistics
{
    class Program
    {
        static PedidoRepository repository = new PedidoRepository();

        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE LOGÍSTICA ===\n");
                Console.WriteLine("1 - Novo pedido");
                Console.WriteLine("2 - Listar pedidos");
                Console.WriteLine("3 - Editar pedido");
                Console.WriteLine("4 - Excluir pedido");
                Console.WriteLine("5 - Sair");
                Console.Write("\nOpção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": CriarPedido(); break;
                    case "2": ListarPedidos(); break;
                    case "3": EditarPedido(); break;
                    case "4": ExcluirPedido(); break;
                    case "5": sair = true; break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void CriarPedido()
        {
            Console.Clear();
            Console.WriteLine("=== NOVO PEDIDO ===\n");

            Console.Write("Cliente: ");
            string cliente = Console.ReadLine();

            // VALIDAÇÃO DO PESO
            double peso = 0;
            bool pesoValido = false;
            while (!pesoValido)
            {
                Console.Write("Peso (kg): ");
                string inputPeso = Console.ReadLine();

                if (double.TryParse(inputPeso, out peso) && peso > 0)
                {
                    pesoValido = true;
                }
                else
                {
                    Console.WriteLine("❌ Peso inválido! Digite um número válido (ex: 10.5 ou 10)");
                }
            }

            // VALIDAÇÃO DA DISTÂNCIA
            double distancia = 0;
            bool distanciaValida = false;
            while (!distanciaValida)
            {
                Console.Write("Distância (km): ");
                string inputDistancia = Console.ReadLine();

                if (double.TryParse(inputDistancia, out distancia) && distancia > 0)
                {
                    distanciaValida = true;
                }
                else
                {
                    Console.WriteLine("❌ Distância inválida! Digite um número válido (ex: 100.5 ou 100)");
                }
            }

            // VALIDAÇÃO DA TRANSPORTADORA
            string opcao = "";
            bool transportadoraValida = false;
            TransportadoraFactory factory = null;
            string nomeTransportadora = "";

            while (!transportadoraValida)
            {
                Console.WriteLine("\nTransportadoras:");
                Console.WriteLine("1 - Correios (mais barato)");
                Console.WriteLine("2 - FedEx (mais rápido)");
                Console.Write("Opção: ");

                opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    factory = new CorreiosFactory();
                    nomeTransportadora = "Correios";
                    transportadoraValida = true;
                }
                else if (opcao == "2")
                {
                    factory = new FedExFactory();
                    nomeTransportadora = "FedEx";
                    transportadoraValida = true;
                }
                else
                {
                    Console.WriteLine("❌ Opção inválida! Digite 1 ou 2.");
                }
            }

            // Usa o Factory Method para calcular frete e prazo
            var (frete, prazo) = factory.CalcularFreteEPrazo(peso, distancia);

            Console.WriteLine($"\n✅ Frete: R$ {frete:F2}");
            Console.WriteLine($"✅ Prazo: {prazo} dias úteis");

            var pedido = new Pedido
            {
                Cliente = cliente,
                Peso = peso,
                Distancia = distancia,
                Transportadora = nomeTransportadora,
                ValorFrete = frete
            };

            repository.Adicionar(pedido);
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void ListarPedidos()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE PEDIDOS ===\n");

            var pedidos = repository.ListarTodos();

            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido cadastrado.");
            }
            else
            {
                foreach (var p in pedidos)
                {
                    Console.WriteLine(p);
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void EditarPedido()
        {
            Console.Clear();
            Console.WriteLine("=== EDITAR PEDIDO ===\n");

            ListarPedidosResumido();

            // VALIDAÇÃO DO ID
            int id = 0;
            bool idValido = false;
            while (!idValido)
            {
                Console.Write("\nID do pedido para editar: ");
                string inputId = Console.ReadLine();

                if (int.TryParse(inputId, out id) && id > 0)
                {
                    idValido = true;
                }
                else
                {
                    Console.WriteLine("❌ ID inválido! Digite um número válido.");
                }
            }

            var pedidoExistente = repository.BuscarPorId(id);
            if (pedidoExistente == null)
            {
                Console.WriteLine("❌ Pedido não encontrado!");
                Console.ReadKey();
                return;
            }

            Console.Write($"Novo cliente ({pedidoExistente.Cliente}): ");
            string cliente = Console.ReadLine();
            if (string.IsNullOrEmpty(cliente)) cliente = pedidoExistente.Cliente;

            // VALIDAÇÃO DO PESO
            double peso = pedidoExistente.Peso;
            Console.Write($"Novo peso ({pedidoExistente.Peso} kg): ");
            string pesoStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(pesoStr))
            {
                while (!double.TryParse(pesoStr, out peso) || peso <= 0)
                {
                    Console.Write($"❌ Peso inválido! Digite um número válido ({pedidoExistente.Peso} kg): ");
                    pesoStr = Console.ReadLine();
                    if (string.IsNullOrEmpty(pesoStr)) break;
                }
                if (!string.IsNullOrEmpty(pesoStr)) peso = double.Parse(pesoStr);
                else peso = pedidoExistente.Peso;
            }

            // VALIDAÇÃO DA DISTÂNCIA
            double distancia = pedidoExistente.Distancia;
            Console.Write($"Nova distância ({pedidoExistente.Distancia} km): ");
            string distStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(distStr))
            {
                while (!double.TryParse(distStr, out distancia) || distancia <= 0)
                {
                    Console.Write($"❌ Distância inválida! Digite um número válido ({pedidoExistente.Distancia} km): ");
                    distStr = Console.ReadLine();
                    if (string.IsNullOrEmpty(distStr)) break;
                }
                if (!string.IsNullOrEmpty(distStr)) distancia = double.Parse(distStr);
                else distancia = pedidoExistente.Distancia;
            }

            // VALIDAÇÃO DA TRANSPORTADORA
            string opcao = "";
            TransportadoraFactory factory = null;
            string nomeTransportadora = pedidoExistente.Transportadora;

            Console.WriteLine("\nTransportadoras:");
            Console.WriteLine("1 - Correios");
            Console.WriteLine("2 - FedEx");
            Console.Write($"Opção atual ({pedidoExistente.Transportadora}): ");

            opcao = Console.ReadLine();

            if (opcao == "1")
            {
                factory = new CorreiosFactory();
                nomeTransportadora = "Correios";
            }
            else if (opcao == "2")
            {
                factory = new FedExFactory();
                nomeTransportadora = "FedEx";
            }
            else if (!string.IsNullOrEmpty(opcao))
            {
                Console.WriteLine("⚠️ Opção inválida! Mantendo transportadora atual.");
                factory = pedidoExistente.Transportadora == "Correios" ? new CorreiosFactory() : new FedExFactory();
            }
            else
            {
                factory = pedidoExistente.Transportadora == "Correios" ? new CorreiosFactory() : new FedExFactory();
            }

            var (frete, _) = factory.CalcularFreteEPrazo(peso, distancia);

            var pedidoAtualizado = new Pedido
            {
                Cliente = cliente,
                Peso = peso,
                Distancia = distancia,
                Transportadora = nomeTransportadora,
                ValorFrete = frete
            };

            if (repository.Atualizar(id, pedidoAtualizado))
            {
                Console.WriteLine("\n✅ Pedido atualizado com sucesso!");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void ExcluirPedido()
        {
            Console.Clear();
            Console.WriteLine("=== EXCLUIR PEDIDO ===\n");

            ListarPedidosResumido();

            // VALIDAÇÃO DO ID
            int id = 0;
            bool idValido = false;
            while (!idValido)
            {
                Console.Write("\nID do pedido para excluir: ");
                string inputId = Console.ReadLine();

                if (int.TryParse(inputId, out id) && id > 0)
                {
                    idValido = true;
                }
                else
                {
                    Console.WriteLine("❌ ID inválido! Digite um número válido.");
                }
            }

            if (repository.Remover(id))
            {
                Console.WriteLine("\n✅ Pedido excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("❌ Pedido não encontrado!");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void ListarPedidosResumido()
        {
            var pedidos = repository.ListarTodos();
            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido cadastrado.");
            }
            else
            {
                foreach (var p in pedidos)
                {
                    Console.WriteLine($"{p.Id} - {p.Cliente} - {p.Transportadora}");
                }
            }
        }
    }
}