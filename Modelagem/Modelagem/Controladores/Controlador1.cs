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

        List<Loja> lojas;
        Loja lojaAtual;

        private int numLoja = 0;

        private Controlador1() { }

        public static Controlador1 Instance {
            get { return instance; }
        }

        public void escolheLoja()
        {
            if (lojas == null) {
                lojas = new List<Loja>();

                Loja loja1 = new Loja(1);
                Loja loja2 = new Loja(2);
                Loja loja3 = new Loja(3);

                lojas.Add(loja1);
                lojas.Add(loja2);
                lojas.Add(loja3);
            }

            Console.Clear();

            Console.WriteLine("Escolha a Loja que deseja acessar: \n");

            Console.WriteLine("1 - Loja 1.");
            Console.WriteLine("2 - Loja 2.");
            Console.WriteLine("3 - Loja 3.");
            Console.WriteLine("4 - Voltar ao Menu Principal.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                numLoja = 1;
                LerEstoqueLoja();
                mostraMenuUC1();
            } else if (input == 2) {
                numLoja = 2;
                LerEstoqueLoja();
                mostraMenuUC1();
            } else if (input == 3) {
                numLoja = 3;
                LerEstoqueLoja();
                mostraMenuUC1();
            } else if (input == 4) {
                ControladorGeral.Instance.MenuPrincipal();
            } else {
                Console.WriteLine("Comando inválido.\n");
                escolheLoja();
            }
        }

        // Menu 1
        public void mostraMenuUC1() {
            Console.Clear();

            Console.WriteLine("Bem-vindo ao sistema de estocagem da loja " + numLoja + ", o que deseja fazer hoje? \n");

            Console.WriteLine("1 - Ver o estoque da loja.");
            Console.WriteLine("2 - Criar lista de pedido diário para a matriz.");
            Console.WriteLine("3 - Ver lista de pedido diário.");
            Console.WriteLine("4 - Voltar ao Menu Principal.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                lojaAtual.displayEstoqueAtual();
            } else if (input == 2) {
                lojaAtual.criarPedido();
            } else if (input == 3) {
                lojaAtual.displayPedidosDiarios();
            } else if (input == 4) {   
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
            lojaAtual = lojas[numLoja - 1];

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            using (StreamReader r = new StreamReader(@"..\..\JSON\CU1_EstoqueLoja" + numLoja + ".json")) {
                string json = r.ReadToEnd();
                lojaAtual.recebeListaEstoque(JsonConvert.DeserializeObject<List<ItemEstoque>>(json));
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
            using (StreamWriter file = File.CreateText(@"..\..\JSON\PedidoDiarioLoja" + numLoja + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, pedidoDiario);
            }

            SalvaRegistroLoja();
        }

        // ----------------

        // Salva o numero da loja que registrou um pedido
        private void SalvaRegistroLoja()    
        {
            for (int i = 0; i < Controlador2.Instance.lojasComPedido.Count; i++) {
                if (Controlador2.Instance.lojasComPedido[i] == lojaAtual) {
                    Controlador2.Instance.lojasComPedido[i] = lojaAtual;
                    return;
                }
            }
            
            Controlador2.Instance.lojasComPedido.Add(lojaAtual);
        }
    }
}
