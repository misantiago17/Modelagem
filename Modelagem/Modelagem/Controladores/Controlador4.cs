using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelagem.Controladores
{
    class Controlador4 {

        private static readonly Controlador4 instance = new Controlador4();

        private List<ItemPedidoLoja> listaLoja = new List<ItemPedidoLoja>();
        private List<ItemPedidoLoja> listaSeparada1 = new List<ItemPedidoLoja>();
        private List<ItemPedidoLoja> listaSeparada2 = new List<ItemPedidoLoja>();
        private List<ItemPedidoLoja> listaSeparada3 = new List<ItemPedidoLoja>();
        private List<ItemPedidoLoja> listaAcertosTransferencia1 = new List<ItemPedidoLoja>();
        private List<ItemPedidoLoja> listaAcertosTransferencia2 = new List<ItemPedidoLoja>();
        private List<ItemPedidoLoja> listaAcertosTransferencia3 = new List<ItemPedidoLoja>();
        private List<ItemPedidoLoja> listaTransferencia = new List<ItemPedidoLoja>();



        public List<Loja> lojas;
        private Loja lojaAtual;

        private Controlador4() {
            lojas = Controlador1.Instance.lojas;
        }

        public static Controlador4 Instance {
            get { return instance; }
        }

        public void escolheLoja() {

            Console.Clear();

            Console.WriteLine("Escolha a Loja que deseja acessar: \n");

            Console.WriteLine("1 - Loja 1.");
            Console.WriteLine("2 - Loja 2.");
            Console.WriteLine("3 - Loja 3.");
            Console.WriteLine("4 - Voltar ao Menu Principal.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                lojaAtual = lojas[0];
                listaLoja = lojaAtual.PedidoDiario.retornaListaPedidosDiarios();
                mostraMenuUC4();
            } else if (input == 2) {
                lojaAtual = lojas[1];
                listaLoja = lojaAtual.PedidoDiario.retornaListaPedidosDiarios();
                mostraMenuUC4();
            } else if (input == 3) {
                lojaAtual = lojas[2];
                listaLoja = lojaAtual.PedidoDiario.retornaListaPedidosDiarios();
                mostraMenuUC4();
            } else if (input == 4) {
                ControladorGeral.Instance.MenuPrincipal();
            } else {
                Console.WriteLine("Comando inválido.\n");
                escolheLoja();
            }
        }

        // Menu 4
        public void mostraMenuUC4() {
            Console.Clear();

            Console.WriteLine("Bem-vindo ao sistema de conferencia de transporte de mercadorias, o que deseja fazer hoje? \n");

            Console.WriteLine("1 - Ver lista de pedido diario da loja.");
            Console.WriteLine("2 - Ver lista de separação da loja.");
            Console.WriteLine("3 - Conferir itens do transporte.");
            Console.WriteLine("4 - Ver lista de pós transporte.");
            Console.WriteLine("5 - Ver lista de acertos.");
            Console.WriteLine("6 - Voltar ao Menu Principal.");

            Console.Write("\nDigite o comando: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                displayListaLoja();
            } else if (input == 2) {
                displayListaSeparacaoLoja();
            } else if (input == 3) {
                //ConfereItensTransporte();
            } else if (input == 4) {
                displayListaTransporte();
            } else if (input == 5) {
                displayListaAcertosLoja();
            } else if (input == 6) {
                ControladorGeral.Instance.MenuPrincipal();
            } else {
                Console.WriteLine("Comando inválido.\n");
                mostraMenuUC4();
            }
        }

        public void voltarAoMenuUC4() {

            Console.WriteLine("\nDeseja voltar ao menu? (Digite 1 caso sim)");
            Console.Write("Digite o comando: ");

            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1) {
                mostraMenuUC4();
            } else {
                Console.WriteLine("Comando inválido. \n");
                voltarAoMenuUC4();
            }
        }

        private void displayListaLoja() {

            Console.Clear();

            if (listaLoja.Count == 0) {

                Console.WriteLine("Não há lista de pedido da loja registrada. \n");
                voltarAoMenuUC4();
                return;

            } else {

                Console.WriteLine("Itens da lista de pedido da loja: \n");

                foreach (ItemPedidoLoja item in listaLoja) {

                    Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                    Console.WriteLine("Quantidade Total do item: " + item.quantidade);
                    Console.WriteLine("-------------------------");
                }
            }

            voltarAoMenuUC4();
        }

        private void displayListaSeparacaoLoja() {

            Console.Clear();

            List<ItemPedidoLoja> lista = new List<ItemPedidoLoja>();

            if (lojaAtual.IDLoja == 1)
                lista = listaSeparada1;
            else if (lojaAtual.IDLoja == 2)
                lista = listaSeparada2;
            else if (lojaAtual.IDLoja == 3)
                lista = listaSeparada3;

            if (lista.Count == 0) {

                Console.WriteLine("Não há lista de pedido da loja registrada. \n");
                voltarAoMenuUC4();
                return;

            } else {

                Console.WriteLine("Itens da lista de pedido da loja: \n");

                foreach (ItemPedidoLoja item in lista) {

                    Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                    Console.WriteLine("Quantidade Total do item: " + item.quantidade);
                    Console.WriteLine("-------------------------");
                }
            }

            voltarAoMenuUC4();
        }

        private void displayListaAcertosLoja() {

            Console.Clear();

            List<ItemPedidoLoja> lista = new List<ItemPedidoLoja>();

            if (lojaAtual.IDLoja == 1)
                lista = listaAcertosTransferencia1;
            else if (lojaAtual.IDLoja == 2)
                lista = listaAcertosTransferencia2;
            else if (lojaAtual.IDLoja == 3)
                lista = listaAcertosTransferencia3;

            if (lista.Count == 0)
                carregaListaAcertosLoja(lojaAtual.IDLoja);

            if (lista.Count == 0) {

                Console.WriteLine("Não há lista de pedido da loja registrada. \n");
                voltarAoMenuUC4();
                return;

            } else {

                Console.WriteLine("Itens da lista de pedido da loja: \n");

                foreach (ItemPedidoLoja item in lista) {

                    Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                    Console.WriteLine("Quantidade Total do item: " + item.quantidade);
                    Console.WriteLine("-------------------------");
                }
            }

            voltarAoMenuUC4();
        }

        private void displayListaTransporte() {

            Console.Clear();

            if (listaTransferencia.Count == 0)
                carregaListaTransferencia();

            if (listaTransferencia.Count == 0) {

                Console.WriteLine("Não há lista de pedido da loja registrada. \n");
                voltarAoMenuUC4();
                return;

            } else {

                Console.WriteLine("Itens da lista de pedido da loja: \n");

                foreach (ItemPedidoLoja item in listaTransferencia) {

                    Console.WriteLine("Código do item: " + item.mercadoria.codigoVenda);
                    Console.WriteLine("Quantidade Total do item: " + item.quantidade);
                    Console.WriteLine("-------------------------");
                }
            }

            voltarAoMenuUC4();
        }

        // entao, cada uma delas tem a sua lista, voce precisa ter uma chence de ver a lista que a loja pediu
        // tbm como ver a lista que ela recebe do controlador 3
        // tbm um registro da lista da transferencia que chegou igual ao controlador 3 so que pra outra coisa
        // e dps como ver a lista que o cara criou

        // Dor de cabeça: ver o json dos acertos que sair daqui, conseguir diferenciar oq não veio dos acertos iniciais para poder reler
        // se pa logo dps dos acertos iniciais ler o Json e se o Json já existir
        // Foda-se só salva e esquece que o json existe

        /*private void ConfereItensTransporte() {

            Console.Clear();
            int input = 0;

            List<ItemPedidoLoja> listaSeparacao = new List<ItemPedidoLoja>();

            if (lojaAtual.IDLoja == 1)
                listaSeparacao = listaAcertosTransferencia1;
            else if (lojaAtual.IDLoja == 2)
                listaSeparacao = listaAcertosTransferencia2;
            else if (lojaAtual.IDLoja == 3)
                listaSeparacao = listaAcertosTransferencia3;

            listaTransferencia;

            if (listaSeparacao.Count == 0) {
                Console.WriteLine("matriz não possuí lista de separação das lojas. \n");
                voltarAoMenuUC4();
                return;
            }

            while (input != -1) {

                Console.WriteLine("\nDigite o código da mercadoria que está confirmando.");
                Console.Write("Digite o código: ");
                int cod = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Digite a quantidade separada do item.");
                Console.Write("Digite a quantidade: ");
                int quantidade = Convert.ToInt32(Console.ReadLine());

                // Cria o item e adiciona ele à lista de Pos separacao
                adicionaListaPosSeparacao(cod, quantidade);

                Console.WriteLine("\nDigite -1 caso queira voltar ao menu, digite outro número caso queira adicionar um novo item");
                Console.Write("Digite o comando: ");
                input = Convert.ToInt32(Console.ReadLine());
            }

            salvaListaAcertosLoja();
            salvaListatransferencia();

            mostraMenuUC3();

        }*/


        // Separa o que chegou para cada loja
        public void separaListaSeparacao() {

            listaSeparada1.Clear();
            listaSeparada2.Clear();
            listaSeparada3.Clear();

            List<ItemPedidoLoja> listaSeparacaoTotal = new List<ItemPedidoLoja>();
            listaSeparacaoTotal = Controlador3.Instance.listaPosSeparacao;

            foreach(ItemPedidoLoja item in listaSeparacaoTotal) {

                // Preferencia de separacao por ordem de loja
                for(int i=0; i<lojas.Count; i++) {

                    foreach (ItemPedidoLoja itemLoja in lojas[i].PedidoDiario.retornaListaPedidosDiarios()) {

                        if (item.mercadoria.codigoVenda == itemLoja.mercadoria.codigoVenda) {

                            // quantidade da lista de separacao é menor ou igual ao pedido
                            if (item.quantidade <= itemLoja.quantidade) {
                                ItemPedidoLoja itemChegou = new ItemPedidoLoja();
                                itemChegou.mercadoria = item.mercadoria;
                                itemChegou.quantidade = item.quantidade;

                                if (i == 0)
                                    listaSeparada1.Add(itemChegou);
                                else if (i == 1)
                                    listaSeparada2.Add(itemChegou);
                                else
                                    listaSeparada3.Add(itemChegou);

                                item.quantidade = 0;

                                // quantidade é maior
                            } else {

                                ItemPedidoLoja itemChegou = new ItemPedidoLoja();
                                itemChegou.mercadoria = item.mercadoria;
                                itemChegou.quantidade = itemLoja.quantidade;

                                if (i == 0)
                                    listaSeparada1.Add(itemChegou);
                                else if (i == 1)
                                    listaSeparada2.Add(itemChegou);
                                else
                                    listaSeparada3.Add(itemChegou);

                                item.quantidade -= itemLoja.quantidade;
                            }
                        }
                    }
                }           
            }

            criaListaAcertosIniciais();
        }

        // cria as listas de acertos com as faltas e excessos vindos da separação
        private void criaListaAcertosIniciais() {

            listaAcertosTransferencia1.Clear();
            listaAcertosTransferencia2.Clear();
            listaAcertosTransferencia3.Clear();


            for(int i = 0; i < lojas.Count; i++) {

                foreach(ItemPedidoLoja itemLoja in lojas[i].PedidoDiario.retornaListaPedidosDiarios()) {

                    bool faltou = true;

                    foreach (ItemPedidoLoja itemRecebido in listaSeparada1) {

                        if (itemLoja.mercadoria.codigoVenda == itemRecebido.mercadoria.codigoVenda) {
                            faltou = false;

                            if (itemLoja.quantidade < itemRecebido.quantidade) {

                                ItemPedidoLoja itemFaltou = new ItemPedidoLoja();
                                itemFaltou.mercadoria = itemRecebido.mercadoria;
                                itemFaltou.quantidade = itemLoja.quantidade - itemRecebido.quantidade;

                                if (i == 0)
                                    listaAcertosTransferencia1.Add(itemFaltou);
                                else if (i == 1)
                                    listaAcertosTransferencia2.Add(itemFaltou);
                                else
                                    listaAcertosTransferencia3.Add(itemFaltou);

                            } else if (itemLoja.quantidade > itemRecebido.quantidade) {

                                ItemPedidoLoja itemExcesso = new ItemPedidoLoja();
                                itemExcesso.mercadoria = itemRecebido.mercadoria;
                                itemExcesso.quantidade = itemRecebido.quantidade - itemLoja.quantidade;

                                if (i == 0)
                                    listaAcertosTransferencia1.Add(itemExcesso);
                                else if (i == 1)
                                    listaAcertosTransferencia2.Add(itemExcesso);
                                else
                                    listaAcertosTransferencia3.Add(itemExcesso);

                            }
                        }
                    }

                    if (faltou) {
                        ItemPedidoLoja itemFaltou = new ItemPedidoLoja();
                        itemFaltou.mercadoria = itemLoja.mercadoria;
                        itemFaltou.quantidade = itemLoja.quantidade;

                        if (i == 0)
                            listaAcertosTransferencia1.Add(itemFaltou);
                        else if (i == 1)
                            listaAcertosTransferencia2.Add(itemFaltou);
                        else
                            listaAcertosTransferencia3.Add(itemFaltou);
                    }
                }

            }

            arrumaLista(listaAcertosTransferencia1);
            arrumaLista(listaAcertosTransferencia2);
            arrumaLista(listaAcertosTransferencia3);
        }

        private void arrumaLista(List<ItemPedidoLoja> lista) {

            // Verifica se tem itens repetidos e junta eles em um único item
            for (int i = 0; i < lista.Count; i++) {
                for (int j = i + 1; j < lista.Count; j++) {
                    if (lista[j].mercadoria.codigoVenda == lista[i].mercadoria.codigoVenda) {
                        lista[i].quantidade += lista[j].quantidade;
                        lista.RemoveAt(j);
                        j -= 1;
                    }
                }
            }
        }

        // ------- JSON -------
        private void salvaListaAcertosLoja(int num) {

        }

        private void salvaListaTransferencia() {

        }

        private void carregaListaAcertosLoja(int num) {

        }

        private void carregaListaTransferencia() {

        }

        // ----------------
    }
}
