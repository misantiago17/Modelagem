using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelagem
{
    class ControladorGeral
    {
        private static readonly ControladorGeral instance = new ControladorGeral();

        private ControladorGeral() { }

        public static ControladorGeral Instance { get { return instance; } }

        public void MenuPrincipal()
        {
            bool valid = false;
            while (!valid) {
                valid = true;

                Console.Clear();
                Console.WriteLine("Qual sistema deseja acessar? \n");

                Console.WriteLine("1 - Sistema de estocagem da loja.");
                Console.WriteLine("2 - Sistema de separação da matriz.");
                Console.WriteLine("3 - ");
                Console.WriteLine("4 - ");
                Console.WriteLine("5 - ");

                Console.Write("\nDigite o comando: ");
                int input = Convert.ToInt32(Console.ReadLine());

                if (input == 1) {
                    Controladores.Controlador1 UC1 = Controladores.Controlador1.Instance;
                    UC1.LerEstoqueLoja();
                    UC1.mostraMenuUC1();
                } else if (input == 2) {
                    Console.WriteLine("A ser implementado");
                } else if (input == 3) {
                    Console.WriteLine("A ser implementado");
                } else if (input == 4) {
                    Console.WriteLine("A ser implementado");
                } else {
                    Console.WriteLine("Comando inválido.\n");
                    valid = false;
                }
            }
        }


    }
}
