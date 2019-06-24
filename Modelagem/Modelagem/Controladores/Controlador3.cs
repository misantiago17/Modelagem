using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Modelagem.Controladores
{
    class Controlador3
    {
        private static readonly Controlador3 instance = new Controlador3();

        private List<ItemPedidoLoja> listaSeparacao = new List<ItemPedidoLoja>();
        public List<ItemPedidoLoja> listaPosSeparacao = new List<ItemPedidoLoja>();
        private List<ItemPedidoLoja> listaAcertos = new List<ItemPedidoLoja>();
        public static Controlador3 Instance {
            get { return instance; }
        }

        // Menu 2
        public void mostraMenuUC3() {
            Console.Clear();

            Console.WriteLine("Bem-vindo ao sistema de conferencia de separação da matriz, o que deseja fazer hoje? \n");

            Console.WriteLine("1 - Ver lista de separação.");
            Console.WriteLine("2 - Conferir itens para separação.");
            Console.WriteLine("3 - Ver lista de acertos.");
            Console.WriteLine("4 - Ver confirmacao da lista de separação.");
            Console.WriteLine("5 - Voltar ao Menu Principal.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                displayListaSeparacao();
            } else if (input == 2) {
                ConfereItensSeparacao();
            } else if (input == 3) {
                displayListaAcertos();
            } else if (input == 4) {
                displayListaPosSeparacao();
            } else if (input == 5) {
                ControladorGeral.Instance.MenuPrincipal();
            } else {
                Console.WriteLine("Comando inválido.\n");
                mostraMenuUC3();
            }
        }

        public void voltarAoMenuUC3() {

            Console.WriteLine("\nDeseja voltar ao menu? (Digite 1 caso sim)");
            Console.Write("Digite o comando: ");

            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                mostraMenuUC3();
            } else {
                Console.WriteLine("Comando inválido. \n");
                voltarAoMenuUC3();
            }
        }

        public void carregaListas() {

            listaSeparacao = new List<ItemPedidoLoja>();
            listaSeparacao = Controlador2.Instance.separacao.retornaListaItens();

            listaPosSeparacao = new List<ItemPedidoLoja>();
            carregaListaPosSeparacao();

            listaAcertos = new List<ItemPedidoLoja>();
            carregaListaAcertos();
        }

        private void displayListaSeparacao() {

            Console.Clear();

            if (listaSeparacao.Count == 0) {

                Console.WriteLine("Não há lista de separação registrada. \n");
                voltarAoMenuUC3();
                return;
            } else {

                Console.WriteLine("Itens da lista de separação: \n");

                foreach (ItemPedidoLoja item in listaSeparacao) {

                    Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                    Console.WriteLine("Quantidade Total do item: " + item.quantidade);
                    Console.WriteLine("-------------------------");
                }
            }

            //verificaCondicaoListaSeparacao();
            voltarAoMenuUC3();
        }

        private void displayListaPosSeparacao() {

            Console.Clear();

            if (listaPosSeparacao.Count == 0) {

                Console.WriteLine("Não há lista de conferencia de separação registrada. \n");
                voltarAoMenuUC3();
                return;

            } else {

                Console.WriteLine("Itens da lista de conferencia: \n");

                foreach (ItemPedidoLoja item in listaPosSeparacao) {

                    Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                    Console.WriteLine("Quantidade Total do item: " + item.quantidade);
                    Console.WriteLine("-------------------------");
                }
            }

            voltarAoMenuUC3();
        }

        private void displayListaAcertos() {

            Console.Clear();

            if (listaSeparacao.Count == 0) {

                Console.WriteLine("Não há lista de acertos registrada. \n");
                voltarAoMenuUC3();
                return;

            } else {

                Console.WriteLine("Itens da lista de acertos: \n");

                foreach (ItemPedidoLoja item in listaAcertos) {

                    Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                    Console.WriteLine("Quantidade Total do item: " + item.quantidade);
                    Console.WriteLine("-------------------------");
                }
            }

            voltarAoMenuUC3();
        }

        private void ConfereItensSeparacao() {

            Console.Clear();
            int input = 0;

            if (listaSeparacao.Count == 0) {
                Console.WriteLine("matriz não possuí lista de separação das lojas. \n");
                voltarAoMenuUC3();
                return;
            }

            while (input != -1) {

                Console.WriteLine("\nDigite o código da mercadoria que está confirmando.");
                Console.Write("Digite o código: ");
                int cod = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Digite a quantidade separada do item.");
                Console.Write("Digite a quantidade: ");
                int quantidade = Convert.ToInt32(Console.ReadLine());

                // Cria o item e adiciona ele à lista de Pos separacao
                adicionaListaPosSeparacao(cod, quantidade);

                Console.WriteLine("\nDigite -1 caso queira voltar ao menu, digite outro número caso queira adicionar um novo item");
                Console.Write("Digite o comando: ");
                input = Convert.ToInt32(Console.ReadLine());
            }

            salvaListaAcertos();
            salvaListaPosSeparacao();

            mostraMenuUC3();

        }

        private void adicionaListaPosSeparacao(int cod, int quant) {

            ItemPedidoLoja item = new ItemPedidoLoja();
            Mercadoria merc = new Mercadoria();
            merc.retornaMercadoria(cod);

            item.mercadoria = merc;
            item.quantidade = quant;

            // Pega item já existente da lista de pos separação
            ItemPedidoLoja aux = new ItemPedidoLoja();

            foreach(ItemPedidoLoja itemLista in listaPosSeparacao) {
                if(itemLista.mercadoria.codigoVenda == cod) {
                    aux = itemLista;
                }
            }

            // Verifica se o codigo escrito está na lista de separação pedida
            ItemPedidoLoja itemSolicitado = new ItemPedidoLoja();

            foreach (ItemPedidoLoja itemPedido in listaSeparacao) {
                if (itemPedido.mercadoria.codigoVenda == cod) {
                    itemSolicitado = itemPedido;
                    break;
                }
            }

            if (itemSolicitado.mercadoria.codigoVenda == 0) {
                Console.WriteLine("\nMercadoria separada não está presente na lista de separação solicitada. Adicionando o item à lista de acertos");
                adicionaListaAcertos(item, item.quantidade, false);
                return;
            }

            // Verifica quantidade do item pedido
            if (aux.quantidade + item.quantidade < itemSolicitado.quantidade) {
                Console.WriteLine("\nQuantidade separada é inferior ao que foi solicitado. Adicionando item à lista de acertos.");
                int falta = itemSolicitado.quantidade - item.quantidade;
                adicionaItemNaListaPosSeparacao(item);
                adicionaListaAcertos(item, falta, true);

            } else if (aux.quantidade + item.quantidade > itemSolicitado.quantidade) {
                Console.WriteLine("\nQuantidade separada é superior ao que foi solicitado. Adicionando a necessário à lista de separação e o restante à lista de acertos.");
                int excesso = aux.quantidade + item.quantidade - itemSolicitado.quantidade;
                item.quantidade = item.quantidade - excesso;
                adicionaItemNaListaPosSeparacao(item);
                adicionaListaAcertos(item, excesso, false);
            } else {
                if (adicionaItemNaListaPosSeparacao(item)) {
                    Console.WriteLine("\nItem confirmado com sucesso.");
                }
            }
        }

        // Remove da matriz e adiciona na lista de separação - retorna false se excede o limite da matriz
        private bool adicionaItemNaListaPosSeparacao(ItemPedidoLoja item) {

            // Retira o item do estoque da matriz
            int quantRetirada = item.mercadoria.RemoveDePosicao(item);

            if (quantRetirada < item.quantidade) {
                Console.WriteLine("\nQuantidade separada excede a quantidade existente na matriz.\nAdicionando máximo possível e o restante faltante à lista de acertos.");

                ItemPedidoLoja itemAcerto = new ItemPedidoLoja();
                itemAcerto.mercadoria = item.mercadoria;
                itemAcerto.quantidade = item.quantidade - quantRetirada;
                listaAcertos.Add(itemAcerto);

                item.quantidade = quantRetirada;
                listaPosSeparacao.Add(item);

                arrumaLista(listaPosSeparacao);
                arrumaLista(listaAcertos);
                return false;
            } else {

                ItemPedidoLoja itemPosSep = new ItemPedidoLoja();
                itemPosSep.mercadoria = item.mercadoria;
                itemPosSep.quantidade = item.quantidade;

                listaPosSeparacao.Add(itemPosSep);
                arrumaLista(listaPosSeparacao);
            }
            return true;
        }

        private void arrumaLista(List<ItemPedidoLoja> lista) {

            // Verifica se tem itens repetidos e junta eles em um único item
            for (int i = 0; i < lista.Count; i++) {
                for (int j = i + 1; j < lista.Count; j++) {
                    if (lista[j].mercadoria.codigoVenda == lista[i].mercadoria.codigoVenda) {
                        lista[i].quantidade += lista[j].quantidade;
                        lista.RemoveAt(j);
                        j -= 1;
                    }
                }
            }
        }


        // adiciona é referente a falta adiciona na pos separacao
        private void adicionaListaAcertos(ItemPedidoLoja item, int quantidadeMod, bool adiciona) {

            if (adiciona)
                item.quantidade = quantidadeMod;
            else
                item.quantidade = (-1)*quantidadeMod;

            listaAcertos.Add(item);
            arrumaLista(listaAcertos);
        }


        // ------- JSON -------

        private void carregaListaPosSeparacao() {

            if (File.Exists(@"..\..\JSON\ListaPosSeparacao.json")) {

                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                using (StreamReader r = new StreamReader(@"..\..\ListaPosSeparacao.json")) {
                    string json = r.ReadToEnd();
                    listaPosSeparacao = JsonConvert.DeserializeObject<List<ItemPedidoLoja>>(json);
                }
            }
        }

        private void carregaListaAcertos() {

            if (File.Exists(@"..\..\JSON\ListaAcertos.json")) {

                JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                using (StreamReader r = new StreamReader(@"..\..\ListaAcertos.json")) {
                    string json = r.ReadToEnd();
                    listaAcertos = JsonConvert.DeserializeObject<List<ItemPedidoLoja>>(json);
                }
            }
        }

        private void salvaListaPosSeparacao() {

            using (StreamWriter file = File.CreateText(@"..\..\ListaPosSeparacao.json")) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, listaPosSeparacao);
            }
        }

        private void salvaListaAcertos() {

            using (StreamWriter file = File.CreateText(@"..\..\ListaAcertos.json")) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, listaAcertos);
            }
        }

        // ----------------

    }
}
