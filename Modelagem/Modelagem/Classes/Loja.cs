using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Modelagem
{

    // Loja le o arquivo json com o estoque dela e avalia o que precisa ser criado
    class Loja
    {
        public void VerificaItensFaltantes()
        {
            lerEstoqueExistente();
        }


        void incluirItemEmPedido(int cod, PedidoLoja pedido)
        {

        }
        static void criarPedido()
        {

        }

        // Fora da arquitetura prevista

        void lerEstoqueExistente()
        {
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            //////
            using (StreamReader r = new StreamReader(@"..\..\CU1_EstoqueLoja1.json"))
            {
                string json = r.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
            }
            dynamic array = serializer.DeserializeObject(json);

                Console.WriteLine("");
                Console.WriteLine(serializer.Serialize(array));
                Console.WriteLine("");
                Console.ReadKey();
            }
            ///////

            /*
            dynamic resultado = serializer.DeserializeObject(json);

            Console.WriteLine("  ==  Resultado da leitura do arquivo JSON  == ");
            Console.WriteLine("");

            foreach (KeyValuePair<string, object> entry in resultado)
            {
                var key = entry.Key;
                var value = entry.Value as string;
                Console.WriteLine(String.Format("{0} : {1}", key, value));
            }

            Console.WriteLine("");
            Console.WriteLine(serializer.Serialize(resultado));
            Console.WriteLine("");
            Console.ReadKey();
            */
        }


    }

    public class Item
    {
        public int cod;
        public int quantidade;
    }
}
