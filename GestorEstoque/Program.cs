using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GestorEstoque
{
    class Program
    {
        static List<IEstoque> produtos = new List<IEstoque>();
        enum Menu { Listar = 1, Adicionar, Remover, Entrada, Saida, Sair}
        
        static void Main(string[] args)
        
        {
            Carregar();
            bool escolheuSair = false;
            while (escolheuSair == false)
            {
                Console.WriteLine("Sistema de estoque:");
                Console.WriteLine("1-Listar\n2-Adicionar\n3-Remover\n4-Entrada\n5-Saida\n6-Sair");
                int opStr = int.Parse(Console.ReadLine());
                
                if (opStr > 0 && opStr <7)
                {
                    Menu escolha = (Menu)opStr;
                    switch (escolha)
                    {
                        case Menu.Listar:
                            Listagem();
                            break;

                        case Menu.Adicionar:
                            Cadastro();
                            break;

                        case Menu.Remover:
                            Remover();
                            break;

                        case Menu.Entrada:
                            Entrada();
                            break;

                        case Menu.Saida:
                            Saida();
                            break;

                        case Menu.Sair:
                            escolheuSair = true;
                            break;
                    }

                }
            }
        }

        static void Listagem() 
        {
            Console.WriteLine("Lista de Produtos");
            int i = 0;
            foreach (IEstoque produto in produtos)
            {
                Console.WriteLine("ID: " + i);
                produto.Exibir();
                i++;
            }
            Console.ReadLine();
        }

        static void Remover() 
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer remover:");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos.RemoveAt(id);
                Salvar();
            }
        }

        static void Entrada()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar entrada:");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarEntrada();
                Salvar();
            }
        }

        static void Saida()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar saida:");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarSaida();
                Salvar();
            }
        }


        enum menuCadastro { ProdutoFisico = 1, Ebook, Curso }
        static void Cadastro()
        {         

            Console.WriteLine("Cadastro de Produto");
            Console.WriteLine("1-Produto Físico\n2-Ebook\n3-Curso");
            int opcao = int.Parse(Console.ReadLine());

            menuCadastro escolha = (menuCadastro)opcao;
            switch (escolha)
            {
                case menuCadastro.ProdutoFisico:
                    CadastrarPfisico();
                    break;
                case menuCadastro.Ebook:
                    CadastrarEbook();
                    break;
                case menuCadastro.Curso:
                    CadastrarCursos();
                    break;
              
            }


        }

        static void CadastrarPfisico()
        {
            Console.WriteLine("Cadastrando produto físico");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.WriteLine("Frete: ");
            float frete = float.Parse(Console.ReadLine());
            ProdutoFisico pf = new ProdutoFisico(nome, preco, frete);
            produtos.Add(pf);
            Salvar();

        }

        static void CadastrarEbook()
        {
            Console.WriteLine("Cadastrando produto físico");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();

            Ebook eb = new Ebook(nome, preco, autor);
            produtos.Add(eb);
            Salvar();

        }
        static void CadastrarCursos()
        {
            Console.WriteLine("Cadastrando produto físico");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();

            Cursos cs = new Cursos(nome, preco, autor);
            produtos.Add(cs);
            Salvar();
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, produtos);

            stream.Close();

        }

        static void Carregar()
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            

            try
            {
                produtos = (List<IEstoque>)encoder.Deserialize(stream);

                if (produtos == null)
                {
                    produtos = new List<IEstoque>();
                }
            }
            catch (Exception)
            {
                produtos = new List<IEstoque>();
                
            }

            stream.Close();
        }



    }

}
