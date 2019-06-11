using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelagem
{
    class ItemPedidoLoja
    {
        public int quantidade;
        public Mercadoria mercadoria = new Mercadoria();

        public ItemPedidoLoja criaNovoItem(int cod, int quantidade) {

            this.mercadoria.retornaMercadoria(cod);
            this.quantidade = quantidade;
            if (mercadoria != null){
                return this;
            }
            return null;
        }
    }
}
