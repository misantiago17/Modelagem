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

        // instanciar UCs como primeira coisa

        public void MenuPrincipal()
        {
            bool valid = false;
            while (!valid) {
                valid = true;

                Console.Clear();
                Console.WriteLine("Qual sistema deseja acessar? \n");

                Console.WriteLine("1 - Sistema de estocagem das lojas.");
                Console.WriteLine("2 - Sistema de separação da matriz.");
                Console.WriteLine("3 - Sistema de conferencia de separação da matriz.");
                Console.WriteLine("4 - Sistema de conferencia de transporte de mercadorias.");

                Console.Write("\nDigite o comando: ");
                int input = Convert.ToInt32(Console.ReadLine());

                if (input == 1) {
                    Controladores.Controlador1 UC1 = Controladores.Controlador1.Instance;
                    UC1.escolheLoja();
                } else if (input == 2) {
                    Controladores.Controlador2 UC2 = Controladores.Controlador2.Instance;
                    UC2.CarregaLojasComPedidoDiario();
                    UC2.mostraMenuUC2();
                } else if (input == 3) {
                    Controladores.Controlador3 UC3 = Controladores.Controlador3.Instance;
                    UC3.carregaListas();
                    UC3.mostraMenuUC3();
                } else if (input == 4) {
                    Controladores.Controlador4 UC4 = Controladores.Controlador4.Instance;
                    UC4.separaListaSeparacao();
                    UC4.escolheLoja();
                } else {
                    Console.WriteLine("Comando inválido.\n");
                    valid = false;
                }
            }
        }


    }
}
