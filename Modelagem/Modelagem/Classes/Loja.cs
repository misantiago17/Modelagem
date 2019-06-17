using System;
using System.Collections.Generic;

namespace Modelagem
{
    // Loja le o arquivo json com o estoque dela e avalia o que precisa ser criado
    class Loja
    {
        private List<ItemEstoque> itemsEstoque;

        public PedidoLoja PedidoDiario = new PedidoLoja();


        // 1 - Ver estoque loja

        // Armazena a lista de itensvinda do controlador
        public void recebeListaEstoque(List<ItemEstoque> estoque) {
            itemsEstoque = estoque;
        }

        public void displayEstoqueAtual() {

            Console.Clear();

            Console.WriteLine("Estoque da Loja: \n");

            foreach (ItemEstoque item in itemsEstoque) {
                Console.WriteLine("Código do item: " + item.cod);
                Console.WriteLine("Quantidade do item: " + item.quantidade);
                Console.WriteLine("-------------------------");
            }

            Controladores.Controlador1.Instance.voltarAoMenuUC1();
        }

        // 2 - Criar lista de pedido diário para a matriz.

        // era static esse método - 2º chamada
        public void criarPedido() {

            PedidoDiario.criarPedido();

            Console.Clear();
            int input = 0;

            while (input != -1){

                Console.WriteLine("\nDigite o código do item que deseja adicionar a lista de pedidos diários.");
                Console.Write("Digite o código: ");
                int cod = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Digite a quantidade que deseja pedir do item.");
                Console.Write("Digite a quantidade: ");
                int quantidade = Convert.ToInt32(Console.ReadLine());

                // adiciona item a um json de pedidos diários
                incluirItemEmPedido(cod, quantidade);

                Console.WriteLine("\nDigite -1 caso queira voltar ao menu, digite outro número caso queira adicionar um novo item");
                Console.Write("Digite o comando: ");
                input = Convert.ToInt32(Console.ReadLine());
            }

            Controladores.Controlador1.Instance.SalvaListaPedidoDiario(PedidoDiario.retornaListaPedidosDiarios());
            Controladores.Controlador1.Instance.mostraMenuUC1();
        }

        private void incluirItemEmPedido(int cod,int  quantidade) {
            PedidoDiario.incluirItemEmPedido(cod, quantidade);
        }

        // 3 - Ver lista de pedido diário.

        public void displayPedidosDiarios()
        {
            Console.Clear();

            Console.WriteLine("Pedidos diários da loja: \n");

            foreach (ItemPedidoLoja item in PedidoDiario.retornaListaPedidosDiarios())
            {
                Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                Console.WriteLine("Quantidade do item: " + item.quantidade);
                Console.WriteLine("-------------------------");
            }

            Controladores.Controlador1.Instance.voltarAoMenuUC1();
        }


    }

    public class ItemEstoque
    {
        public int cod;
        public int quantidade;
    }
}
