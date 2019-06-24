using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Modelagem
{
    class PedidoLoja
    {
        private List<ItemPedidoLoja> itemsPedidosDiarios;
        DateTime data;

        public void criarPedido(int numLoja) {

            itemsPedidosDiarios = new List<ItemPedidoLoja>();

            itemsPedidosDiarios.Clear();
            CarregaListaPedidoDiario(numLoja);

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

        private void arrumaLista() {

            // Verifica se tem itens repetidos e junta eles em um único item
            for (int i = 0; i < itemsPedidosDiarios.Count; i++) {
                ItemPedidoLoja itemVerifica = itemsPedidosDiarios[i];

                for (int j = i + 1; j < itemsPedidosDiarios.Count; j++) {
                    if (itemsPedidosDiarios[j].mercadoria.codigoVenda == itemsPedidosDiarios[i].mercadoria.codigoVenda) {
                        itemsPedidosDiarios[i].quantidade += itemsPedidosDiarios[j].quantidade;
                        itemsPedidosDiarios.RemoveAt(j);
                        j -= 1;
                    }
                }
            }

        }

        // ------- JSON -------

        public void CarregaListaPedidoDiario(int numLoja) {

            if (File.Exists(@"..\..\PedidoDiarioLoja" + numLoja + ".json")) {
                List<ItemPedidoLoja> aux = new List<ItemPedidoLoja>();

                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                using (StreamReader r = new StreamReader(@"..\..\PedidoDiarioLoja" + numLoja + ".json")) {
                    string json = r.ReadToEnd();
                    aux = JsonConvert.DeserializeObject<List<ItemPedidoLoja>>(json);
                }

                foreach (ItemPedidoLoja item in aux) {
                    itemsPedidosDiarios.Add(item);
                }
            }

            arrumaLista();
        }

        // ----------------

    }
}
