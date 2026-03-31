# FastLogistics - Sistema de Logística

Sistema simples de logística desenvolvido em C# para demonstrar a aplicação dos princípios SOLID e do padrão Factory Method.

## Funcionalidades

- Cadastrar pedidos com cliente, peso e distância
- Calcular frete automaticamente (Correios ou FedEx)
- Listar todos os pedidos
- Editar pedidos existentes
- Excluir pedidos

## Transportadoras

| Transportadora | Fórmula | Prazo |
|----------------|---------|-------|
| Correios | (peso × 2,0) + (distância × 0,5) | 5 dias |
| FedEx | (peso × 3,0) + (distância × 0,8) | 3 dias |

## Estrutura do Projeto

```
FastLogistics/
├── Models/
│   ├── ITransportadora.cs      # Interface
│   ├── Correios.cs              # Implementação
│   ├── FedEx.cs                 # Implementação
│   └── Pedido.cs                # Modelo de dados
├── Factories/
│   ├── TransportadoraFactory.cs # Factory Method (abstrata)
│   ├── CorreiosFactory.cs       # Fábrica concreta
│   └── FedExFactory.cs          # Fábrica concreta
├── Repositories/
│   └── PedidoRepository.cs      # CRUD
└── Program.cs                   # Interface com usuário
```

## Como Executar

```bash
# Clonar o repositório
git clone https://github.com/jopimentell/projeto-transport

# Acessar a pasta
cd FastLogistics

# Executar
dotnet run
```

## Pré-requisitos

- .NET 8.0 ou superior

## Padrões Aplicados

- **SOLID** - Todos os 5 princípios
- **Factory Method** - Criação encapsulada de transportadoras

Desenvolvido para o TDE da disciplina de Padrões de Projeto de Software - UNIFACEMA
