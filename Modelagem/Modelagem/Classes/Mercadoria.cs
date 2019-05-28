using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelagem
{
    class Mercadoria
    {
        int codigoVenda;

        // Códgi ode armazenamento é apenas para a matriz
        int codigoArmazenamento;

        bool avaliaCodigoMercValida(int cod)
        {
            return true;
        }

        Mercadoria retornaMercadoria(int cod)
        {
            return this;
        }

        int retornaPosicao(int cod)
        {
            return 0;
        }

        static bool verificaExistenciaMerc(int cod)
        {
            return true;
        }

    }
}
