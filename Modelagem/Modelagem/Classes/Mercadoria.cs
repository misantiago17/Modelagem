using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


namespace Modelagem
{
    class Mercadoria 
    {
        [JsonProperty("codVenda")]
        public int codigoVenda;

        // Estante em que se encontra a mercadoria
        [JsonProperty("estante")]
        public int codEstante;

        // posicao nas estantes no qual se apresenta a mercadoria
        [JsonProperty("posicoes")]
        public List<int> codPosicoes;
        public int quantidade;

        public Estante CodEstante = new Estante();
        public Posicao CodPosicoes = new Posicao();

        public Mercadoria retornaMercadoria(int cod)
        {
            // Verificar em mercadoria se o código de venda do item existe mesmo
            if (avaliaCodigoMercValida(cod)) {

                foreach (Mercadoria mercadoria in EstoqueMercadoriaMatriz.Instance.mercadoriasExistentes) {
                    if (mercadoria.codigoVenda == cod) {
                        codigoVenda = mercadoria.codigoVenda;
                        CodPosicoes.identificacao = mercadoria.codPosicoes;
                        CodPosicoes.quantidade = mercadoria.quantidade;
                        CodEstante.identificacao = mercadoria.codEstante;
                    }
                }

                return this;
            } else {
                Console.WriteLine("Mercadoria Inválida.");
            }
            return null;
        }

        // Avalia se a mercadoria existe
        private bool avaliaCodigoMercValida(int cod) 
        {
            if (EstoqueMercadoriaMatriz.Instance.mercadoriasExistentes == null) {
                EstoqueMercadoriaMatriz.Instance.carregaMercadorias();
            }

            foreach (Mercadoria mercadoria in EstoqueMercadoriaMatriz.Instance.mercadoriasExistentes) {

                if (mercadoria.codigoVenda == cod) {
                    return true;
                }
            }
            return false;
        }

        // Não tá tirando certo do json a quantidade certa

        public int RemoveDePosicao(ItemPedidoLoja item) {

            Mercadoria itemOriginal = item.mercadoria.retornaMercadoria(item.mercadoria.codigoVenda);

            // excede a quantidade da matriz
            if (itemOriginal.CodPosicoes.quantidade < item.quantidade) {
                itemOriginal.CodPosicoes.quantidade = 0;
                EstoqueMercadoriaMatriz.Instance.salvaJsonPosicoes(itemOriginal);
                return item.quantidade - (item.quantidade - 100);
            } else {
                itemOriginal.CodPosicoes.quantidade -= item.quantidade;
                EstoqueMercadoriaMatriz.Instance.salvaJsonPosicoes(itemOriginal);
                return item.quantidade;
            }
        }


    }

    class EstoqueMercadoriaMatriz
    {
        private static readonly EstoqueMercadoriaMatriz instance = new EstoqueMercadoriaMatriz();

        private EstoqueMercadoriaMatriz() { }

        public static EstoqueMercadoriaMatriz Instance {
            get { return instance; }
        }

        public List<Mercadoria> mercadoriasExistentes;
        public List<AuxPosicao> posAux = new List<AuxPosicao>();

        // ------- JSON -------

        public void carregaMercadorias() {

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            using (StreamReader r = new StreamReader(@"..\..\JSON\MercadoriaRegistro.json"))
            {
                string json = r.ReadToEnd();
                mercadoriasExistentes = JsonConvert.DeserializeObject<List<Mercadoria>>(json);
            }

            carregaJsonPosicoes();
        }

        // salva numa lista de posicoes, arruma a posicao dentro de cada mercadoria 
        private void carregaJsonPosicoes() {

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            using (StreamReader r = new StreamReader(@"..\..\JSON\Posicoes.json")) {
                string json = r.ReadToEnd();
                posAux = JsonConvert.DeserializeObject<List<AuxPosicao>> (json);
            }

            foreach(AuxPosicao position in posAux) {
                foreach (Mercadoria merc in mercadoriasExistentes) {

                    if (merc.codEstante == position.estante) {
                        if (merc.codPosicoes[0] == position.posicao || merc.codPosicoes[1] == position.posicao) {
                            merc.quantidade += position.quantidade;
                        }
                    }
                }
            } 
        }

        // isso precisa carregar o que está em mercadoria
        public void salvaJsonPosicoes(Mercadoria merc) {    
            for (int i=0;i < posAux.Count; i++) {
                if (merc.CodEstante.identificacao == posAux[i].estante) {
                    if (merc.CodPosicoes.identificacao[0] == posAux[i].posicao || merc.CodPosicoes.identificacao[1] == posAux[i].posicao) {
                        if (merc.CodPosicoes.quantidade >= 50) {
                            if (merc.CodPosicoes.identificacao[0] == posAux[i].posicao) {
                                posAux[i].quantidade = 50;
                            }
                            if (merc.CodPosicoes.identificacao[1] == posAux[i].posicao) {
                                posAux[i].quantidade = merc.CodPosicoes.quantidade - 50;
                            }
                        } else {
                            if (merc.CodPosicoes.identificacao[0] == posAux[i].posicao) {
                                posAux[i].quantidade = merc.CodPosicoes.quantidade;
                            }
                            if (merc.CodPosicoes.identificacao[1] == posAux[i].posicao) {
                                posAux[i].quantidade = 0;
                            }
                        }
                    }
                }
            }

            // salva por cima do JSON das posicoes
            using (StreamWriter file = File.CreateText(@"..\..\JSON\Posicoes.json")) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, posAux);
            }
        }

        // ----------------
    }

    class AuxPosicao {

        public int posicao;
        public int estante;
        public int quantidade;

    }

}
