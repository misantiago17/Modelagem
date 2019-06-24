using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Modelagem.Controladores
{
    class Controlador2
    {
        private static readonly Controlador2 instance = new Controlador2();

        // Lista com as lojas que possuem pedidos para a matriz
        public List<Loja> lojasComPedido = new List<Loja>();

        public ListaSeparacao separacao = new ListaSeparacao();

        public static Controlador2 Instance {
            get { return instance; }
        }

        // Menu 2
        public void mostraMenuUC2()
        {
            Console.Clear();

            Console.WriteLine("Bem-vindo ao sistema de separação da matriz, o que deseja fazer hoje? \n");

            Console.WriteLine("1 - Lista das lojas.");
            Console.WriteLine("2 - Criar e ver lista de separação para as lojas.");
            Console.WriteLine("3 - Voltar ao Menu Principal.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                displayPedidosLojas();
            } else if (input == 2) {
                adicionaListaPedidos();
            } else if (input == 3) {
                ControladorGeral.Instance.MenuPrincipal();
            } else {
                Console.WriteLine("Comando inválido.\n");
                mostraMenuUC2();
            }
        }

        public void voltarAoMenuUC2()
        {

            Console.WriteLine("\nDeseja voltar ao menu? (Digite 1 caso sim)");
            Console.Write("Digite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1) {
                mostraMenuUC2();
            } else {
                Console.WriteLine("Comando inválido. \n");
                voltarAoMenuUC2();
            }
        }

        private void displayPedidosLojas()
        {
            Console.Clear(); 

            if (lojasComPedido.Count == 0) {

                foreach(Loja loja in lojasComPedido) {

                }
                Console.WriteLine("Não há lista de pedidos registrada. \n");

            } else {

                foreach (Loja loja in lojasComPedido) {

                    Console.WriteLine("\nEstoque da Loja " + loja.IDLoja + ": \n");

                    foreach (ItemPedidoLoja item in loja.PedidoDiario.retornaListaPedidosDiarios()) {

                        Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                        Console.WriteLine("Quantidade do item: " + item.quantidade);
                        Console.WriteLine("-------------------------");
                    }
                }
            }

            voltarAoMenuUC2();
        }

        private void displaySeparacao() {

            Console.Clear();

            if (separacao.retornaListaItens().Count == 0) {

                Console.WriteLine("Não há lista de separação registrada. \n");

            } else {

                Console.WriteLine("Itens da lista de separação: \n");

                foreach (ItemPedidoLoja item in separacao.retornaListaItens())  {

                    Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                    Console.WriteLine("Quantidade Total do item: " + item.quantidade);
                    Console.WriteLine("-------------------------");
                }
            }

            voltarAoMenuUC2();
        }

        // Bug adiciona duas vezes 
        void adicionaListaPedidos() {

            List<ItemPedidoLoja> aux = new List<ItemPedidoLoja>();

            // Confere para ver se não está adicionando 2 vezes a mesma lista
            foreach (Loja loja in lojasComPedido) {
                foreach(ItemPedidoLoja item in loja.PedidoDiario.retornaListaPedidosDiarios()) {
                    aux.Add(item);
                }
            }

            List<ItemPedidoLoja> ListaAux = new List<ItemPedidoLoja>(aux);

            foreach (ItemPedidoLoja item in aux) {
                foreach (ItemPedidoLoja itemOriginal in separacao.retornaListaItens()) {
                    if(item.mercadoria.codigoVenda == itemOriginal.mercadoria.codigoVenda) {
                        Console.WriteLine("AAAAAAAAA");
                        if (item.quantidade == itemOriginal.quantidade) {
                            // Item é repetido, retira da listaAux
                            ListaAux.Remove(item);
                        }
                    }
                }
            }

            separacao.adicionaLista(ListaAux);

            separacao.salvaListaSeparacao();
            displaySeparacao();
        }


        // ------- JSON -------

        public void CarregaLojasComPedidoDiario() {

            for(int numloja = 1; numloja <= 3; numloja++) {

                if (File.Exists(@"..\..\PedidoDiarioLoja" + numloja + ".json")) {

                    lojasComPedido.Add(Controlador1.Instance.lojas[numloja - 1]);
                }
            }

           
        }

        // ----------------
    }
}
