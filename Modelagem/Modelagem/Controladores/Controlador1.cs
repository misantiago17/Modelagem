using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;



namespace Modelagem.Controladores
{
    class Controlador1
    {
        private static readonly Controlador1 instance = new Controlador1();

        //JSON listaPedidoLoja;
        Loja loja = new Loja();

        private Controlador1() { }

        public static Controlador1 Instance {
            get { return instance; }
        }

        // Menu 1
        public void mostraMenuUC1() {
            Console.Clear();

            Console.WriteLine("Bem-vindo ao sistema de estocagem da loja, o que deseja fazer hoje? \n");

            Console.WriteLine("1 - Ver o estoque da loja.");
            Console.WriteLine("2 - Criar lista de pedido diário para a matriz.");
            Console.WriteLine("3 - Ver lista de pedido diário.");
            Console.WriteLine("4 - Enviar lista de pedido diário para a matriz.");
            Console.WriteLine("5 - Voltar ao Menu Principal.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                loja.displayEstoqueAtual();
            } else if (input == 2) {
                loja.criarPedido();
            } else if (input == 3) {
                loja.displayPedidosDiarios();
            } else if (input == 4) {
                // Toda vez que enviar a lista para a matriz, ele cria uma nova lista limpa por cima da antiga para ser enviada para matriz caso entre nesse menu novamente
                // Se ele sair do menu sem enviar a lista ele perde a lista
                Console.WriteLine("A ser implementado");
            } else if (input == 5) {
                ControladorGeral.Instance.MenuPrincipal();
            } else {
                Console.WriteLine("Comando inválido.\n");
                mostraMenuUC1();
            }
        }

        public void voltarAoMenuUC1() {

            Console.WriteLine("\nDeseja voltar ao menu? (Digite 1 caso sim)");
            Console.Write("Digite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1) {
                mostraMenuUC1();
            } else {
                Console.WriteLine("Comando inválido. \n");
                voltarAoMenuUC1();
            }
        }

        // ------- JSON -------
        public void LerEstoqueLoja()
        {
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            using (StreamReader r = new StreamReader(@"..\..\CU1_EstoqueLoja1.json")) {
                string json = r.ReadToEnd();
                loja.recebeListaEstoque(JsonConvert.DeserializeObject<List<ItemEstoque>>(json));
            }
        }


        public void SalvaListaPedidoDiario(List<ItemPedidoLoja> pedidoDiario)
        {
            // Verifica se tem itens repetidos e junta eles em um único item
            for(int i=0; i < pedidoDiario.Count; i++) {
                ItemPedidoLoja itemVerifica = pedidoDiario[i];

                for(int j=i+1; j < pedidoDiario.Count; j++) {
                    if (pedidoDiario[j].mercadoria.codigoVenda == pedidoDiario[i].mercadoria.codigoVenda) {
                        pedidoDiario[i].quantidade += pedidoDiario[j].quantidade;
                        pedidoDiario.RemoveAt(j);
                        j -= 1;
                    }
                }
            }

            // Salva itens no Json de pedidos diários
            using (StreamWriter file = File.CreateText(@"..\..\PedidoDiarioLoja1.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, pedidoDiario);
            }

        }

        void SalvaRegistroLoja()    // O que era pra ser isso??
        {

        }
    }
}
