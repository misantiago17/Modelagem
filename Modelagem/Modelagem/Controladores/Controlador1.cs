using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;



namespace Modelagem.Controladores
{
    class Controlador1
    {
        //JSON listaPedidoLoja;
        Loja loja = new Loja();

        // Menu 1
        public void mostraMenuUC1() {
            Console.Clear();

            Console.WriteLine("Bem-vindo ao sistema de estocagem da loja, o que deseja fazer hoje? \n");

            Console.WriteLine("1 - Ver o estoque da loja.");
            Console.WriteLine("2 - Criar lista de pedido diário para a matriz.");
            Console.WriteLine("3 - Ver lista de pedido diário.");
            Console.WriteLine("4 - Enviar lista de pedido diário para a matriz.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                loja.displayEstoqueAtual();
            } else if (input == 2) {
                loja.criarPedido();
            } else if (input == 3) {
                loja.displayPedidosDiarios();
            } else if (input == 4) {
                Console.WriteLine("A ser implementado");
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
                Console.WriteLine("\n Não entendi o comando. \n");
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
            // Salva itens no Json de pedidos diários - ideal é verificar se já existe pedido com o mesmo codigo e somar a ele a quantidade desejada - perguntar se é necessario
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
