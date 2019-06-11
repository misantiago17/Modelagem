using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelagem
{
    class Mercadoria // -------------------- TO DO
    {
        public int codigoVenda;

        // Código onde armazenamento é apenas para a matriz
        int codigoArmazenamento;

        public Mercadoria retornaMercadoria(int cod)
        {
            // Verificar em mercadoria se o código de venda do item existe mesmo
            avaliaCodigoMercValida(cod);
            if (avaliaCodigoMercValida(cod)) {
                codigoVenda = cod;
                codigoArmazenamento = 0; // TO DO

                return this;
            } else {
                Console.WriteLine("Mercadoria Inválida.");
            }
            return null;
        }
        private bool avaliaCodigoMercValida(int cod)    // ------------ TO DO - perguntar para ele
        {
            return true;
        }

        private int retornaPosicao(int cod)
        {
            return 0;
        }

        private static bool verificaExistenciaMerc(int cod)
        {
            return true;
        }

    }
}
