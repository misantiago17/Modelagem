using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelagem
{
    class PedidoLoja
    {
        private List<ItemPedidoLoja> itemsPedidosDiarios;
        DateTime data;

        public void criarPedido() {

            itemsPedidosDiarios = new List<ItemPedidoLoja>();
            data = DateTime.Now;
        }

        public void incluirItemEmPedido(int cod, int quantidade) {

            ItemPedidoLoja pedido = new ItemPedidoLoja();
            pedido = pedido.criaNovoItem(cod, quantidade);
            if(pedido != null) {
                itemsPedidosDiarios.Add(pedido);
            } 

        }

        public List<ItemPedidoLoja> retornaListaPedidosDiarios() {
            return itemsPedidosDiarios;
        }

    }
}
