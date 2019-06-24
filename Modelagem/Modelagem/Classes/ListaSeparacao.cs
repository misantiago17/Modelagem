using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Modelagem
{
    class ListaSeparacao
    {
        List<ItemPedidoLoja> listaSeparacao;

        public ListaSeparacao() {

            listaSeparacao = new List<ItemPedidoLoja>();
            LeListaSeparacao();
        }

        public List<ItemPedidoLoja> retornaListaItens() {

            return listaSeparacao;
        }

        public void adicionaLista(List<ItemPedidoLoja> listaLoja) {

            foreach (ItemPedidoLoja item in listaLoja) {
                listaSeparacao.Add(item);
            }

            arrumaLista();
        }

        void adicionaListaAcertos(int cod)
        {

        }

        // Junta os itens repetidos da lista
        private void arrumaLista() {

            for (int i = 0; i < listaSeparacao.Count; i++)
            {
                ItemPedidoLoja itemVerifica = listaSeparacao[i];

                for (int j = i + 1; j < listaSeparacao.Count; j++)
                {
                    if (listaSeparacao[j].mercadoria.codigoVenda == listaSeparacao[i].mercadoria.codigoVenda) {

                        listaSeparacao[i].quantidade += listaSeparacao[j].quantidade;
                        listaSeparacao.RemoveAt(j);
                        j -= 1;
                    }
                }
            }
        }

        // ------- JSON -------

        private void LeListaSeparacao() {

            if (File.Exists(@"..\..\SeparacaoMatriz.json")) {

                List<ItemPedidoLoja> aux = new List<ItemPedidoLoja>();

                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                using (StreamReader r = new StreamReader(@"..\..\SeparacaoMatriz.json")) {
                    string json = r.ReadToEnd();
                    aux = JsonConvert.DeserializeObject<List<ItemPedidoLoja>>(json);
                }

                foreach (ItemPedidoLoja item in aux) {
                    listaSeparacao.Add(item);
                }

                arrumaLista();
            }
        }

        public void salvaListaSeparacao() {

            // Salva itens no Json de pedidos diários
            using (StreamWriter file = File.CreateText(@"..\..\SeparacaoMatriz.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, listaSeparacao);
            }
        }

        // ----------------
    }
}
