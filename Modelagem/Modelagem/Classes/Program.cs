using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelagem
{
    class Program
    {
        static void Main(string[] args)
        {

            Controladores.Controlador1 UC1 = new Controladores.Controlador1();
            UC1.LerEstoqueLoja();
            UC1.mostraMenuUC1();

            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
