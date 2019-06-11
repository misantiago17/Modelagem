using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelagem.Controladores
{
    class Controlador2
    {
        private static readonly Controlador2 instance = new Controlador2();

        //JSON listaPedidoLojas[];

        private Controlador2() { }

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
            Console.WriteLine("3 - Solicitar retirada de itens.");
            Console.WriteLine("4 - Voltar ao Menu Principal.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                Console.WriteLine("A ser implementado");
            } else if (input == 2) {
                Console.WriteLine("A ser implementado");
            } else if (input == 3) {
                Console.WriteLine("A ser implementado");
            } else if (input == 4) {
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
            if (input == 1)
            {
                mostraMenuUC2();
            }
            else
            {
                Console.WriteLine("Comando inválido. \n");
                voltarAoMenuUC2();
            }
        }

        /*
            Sistema recebe os pedidos diários de todas as lojas.
            Sistema cria lista de itens chamada “Lista de Separação”
            Sistema solicita a retirada dos itens nas posiçoes relacionadas das mercadorias nas quantidades desejadas
         */

        void salvaListaJson()
        {

        }

        void adicionaListaPedidos(List<ItemPedidoLoja> listaPedidoLoja)
        {

        }

    }
}
