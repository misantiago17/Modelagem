using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Modelagem
{

    // Loja le o arquivo json com o estoque dela e avalia o que precisa ser criado
    class Loja
    {
        private List<Item> itemsEstoque = new List<Item>();
        private List<Item> itemsPedidosDiarios = new List<Item>();

        public void mostraMenuUC1()
        {
            Console.Clear();

            Console.WriteLine("Bem-vindo ao sistema de estocagem da loja, o que deseja fazer hoje? \n");

            Console.WriteLine("1 - Ver o estoque da loja.");
            Console.WriteLine("2 - Criar lista de pedido diário para a matriz.");
            Console.WriteLine("3 - Ver lista de pedido diário.");
            Console.WriteLine("4 - Enviar lista de pedido diário para a matriz.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                displayEstoqueAtual();
            } else if (input == 2) {
                criarPedido();
            } else if (input == 3) {
                displayPedidosDiarios();
            } else if (input == 4) {
                Console.WriteLine("A ser implementado");
            } else {
                Console.WriteLine("Comando inválido.\n");
                mostraMenuUC1();
            }
        }

        public void VerificaItensFaltantes()
        {
            lerEstoqueExistente();
        }


        private void incluirItemEmPedido(int cod, PedidoLoja pedido)
        {

        }

        // era static esse método
        private void criarPedido()
        {
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
                Item pedido = new Item();
                pedido.cod = cod;
                pedido.quantidade = quantidade;
                itemsPedidosDiarios.Add(pedido);

                Console.WriteLine("\nDigite -1 caso queira voltar ao menu, digite outro número caso queira adicionar um novo item");
                Console.Write("Digite o comando: ");
                input = Convert.ToInt32(Console.ReadLine());
            }

            // Salva itens no Json de pedidos diários - ideal é verificar se já existe pedido com o mesmo codigo e somar a ele a quantidade desejada
            using (StreamWriter file = File.CreateText(@"..\..\PedidoDiarioLoja1.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, itemsPedidosDiarios);
            }

            mostraMenuUC1();
        }

        // Fora da arquitetura prevista

        // Lê o arquivo json com o estoque da loja
        private void lerEstoqueExistente()
        {
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            
            using (StreamReader r = new StreamReader(@"..\..\CU1_EstoqueLoja1.json"))
            {
                string json = r.ReadToEnd();
                itemsEstoque = JsonConvert.DeserializeObject<List<Item>>(json);        
            }
        }


        private void displayEstoqueAtual()
        {
            Console.Clear();

            Console.WriteLine("Estoque da Loja: \n");

            foreach (Item item in itemsEstoque)
            {
                Console.WriteLine("Código do item: " + item.cod);
                Console.WriteLine("Quantidade do item: " + item.quantidade);
                Console.WriteLine("-------------------------");
            }

            voltarAoMenuUC1();
        }

        private void displayPedidosDiarios()
        {
            Console.Clear();

            Console.WriteLine("Pedidos diários da loja: \n");

            foreach (Item item in itemsPedidosDiarios)
            {
                Console.WriteLine("Código do item: " + item.cod);
                Console.WriteLine("Quantidade do item: " + item.quantidade);
                Console.WriteLine("-------------------------");
            }

            voltarAoMenuUC1();
        }

        private void voltarAoMenuUC1()
        {
            Console.WriteLine("\nDeseja voltar ao menu? (Digite 1 caso sim)");
            Console.Write("Digite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1) {
                mostraMenuUC1();
            } else {
                Console.WriteLine("\n Não entendi o comando. \n");
                voltarAoMenuUC1();
            }
        }


    }

    public class Item
    {
        public int cod;
        public int quantidade;
    }
}
