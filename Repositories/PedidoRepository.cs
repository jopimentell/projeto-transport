using System;
using System.Collections.Generic;
using System.Linq;
using FastLogistics.Models;

namespace FastLogistics.Repositories
{
    public class PedidoRepository
    {
        private List<Pedido> pedidos = new List<Pedido>();
        private int proximoId = 1;
        
        // CREATE
        public void Adicionar(Pedido pedido)
        {
            pedido.Id = proximoId++;
            pedido.Data = DateTime.Now;
            pedidos.Add(pedido);
            Console.WriteLine("\n✓ Pedido adicionado com sucesso!");
        }
        
        // READ
        public List<Pedido> ListarTodos()
        {
            return pedidos;
        }
        
        public Pedido BuscarPorId(int id)
        {
            return pedidos.FirstOrDefault(p => p.Id == id);
        }
        
        // UPDATE
        public bool Atualizar(int id, Pedido pedidoAtualizado)
        {
            var pedido = BuscarPorId(id);
            if (pedido == null) return false;
            
            pedido.Cliente = pedidoAtualizado.Cliente;
            pedido.Peso = pedidoAtualizado.Peso;
            pedido.Distancia = pedidoAtualizado.Distancia;
            pedido.Transportadora = pedidoAtualizado.Transportadora;
            pedido.ValorFrete = pedidoAtualizado.ValorFrete;
            
            return true;
        }
        
        // DELETE
        public bool Remover(int id)
        {
            var pedido = BuscarPorId(id);
            if (pedido == null) return false;
            
            pedidos.Remove(pedido);
            return true;
        }
    }
}