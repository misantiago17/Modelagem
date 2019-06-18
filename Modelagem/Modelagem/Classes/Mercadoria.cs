using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


namespace Modelagem
{
    class Mercadoria 
    {
        public int codigoVenda;

        // Código onde armazenamento é apenas para a matriz
        public int[] codigoArmazenamento;

        public Mercadoria retornaMercadoria(int cod)
        {
            // Verificar em mercadoria se o código de venda do item existe mesmo
            if (avaliaCodigoMercValida(cod)) {

                foreach (Mercadoria mercadoria in EstoqueMercadoriaMatriz.Instance.mercadoriasExistentes) {
                    if (mercadoria.codigoVenda == cod) {
                        codigoVenda = mercadoria.codigoVenda;
                        codigoArmazenamento = mercadoria.codigoArmazenamento;
                    }
                }

                return this;
            } else {
                Console.WriteLine("Mercadoria Inválida.");
            }
            return null;
        }
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

        private int retornaPosicao(int cod)
        {
            return 0;
        }

        private static bool verificaExistenciaMerc(int cod)
        {
            return true;
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

        public void carregaMercadorias() {

            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            using (StreamReader r = new StreamReader(@"..\..\JSON\MercadoriaRegistro.json"))
            {
                string json = r.ReadToEnd();
                mercadoriasExistentes = JsonConvert.DeserializeObject<List<Mercadoria>>(json);
            }

            foreach (Mercadoria mercadoria in EstoqueMercadoriaMatriz.Instance.mercadoriasExistentes) {

                Console.WriteLine(mercadoria.codigoVenda + " Venda");
                Console.WriteLine(mercadoria.codigoArmazenamento + " Armazenamento");
            }
        }
    }

}
