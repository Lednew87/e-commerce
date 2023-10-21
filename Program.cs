using Commerce.Domain;
using Commerce.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        //Verificar migrations pendentes

        //using var db = new Commerce.Data.ApplicationContext();

        //var existe = db.Database.GetPendingMigrations().Any();
        //if (existe)
        //{

        //}

        //Método de inserção de um único dado

        static void InserirDados()
        {
            var produto = new Produtos
            {
                Descricao = "Produto teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };
            using var db = new Commerce.Data.ApplicationContext();
            db.Produtos.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");

        }
        static void InserirDadosEmMassa()
        {
            var produtos = new Produtos
            {
                Descricao = "Produto teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente1 = new Cliente
            {
                Nome = "Diana Tonzar",
                CEP = "17285100",
                Cidade = "Vancouver",
                Estado = "CA",
                Telefone = "988774520"
            };
             var cliente2 = new Cliente
             {
                 Nome = "Karina Tonzar",
                 CEP = "17285100",
                 Cidade = "Quebec",
                 Estado = "CA",
                 Telefone = "988786321"
             };
            using var db = new Commerce.Data.ApplicationContext();
            db.AddRange(produtos, cliente1, cliente2);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }
        static void ConsultarDados()
        {
            using var db = new Commerce.Data.ApplicationContext();

            //    Não Funcionou
            //    var consultaPorSintaxe = ("from in c db.Clientes where c.Id>0 select c").ToList();

            //    Consulta para ir direto no database sem utilizar dados de memória
            //    var consultaPorMetodo = db.Clientes.AsNoTracking().Where(p => p.Id > 0).ToList();
            var consultaPorMetodo = db.Clientes.Where(p => p.Id > 0).ToList();

            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consulta de cliente: {cliente.Id}");
                //   Método consulta em memória
                db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }
        static void CadastrarPedido()
        {
            using var db = new Commerce.Data.ApplicationContext();

            var clientes = db.Clientes.FirstOrDefault();
            var produtos = db.Produtos.FirstOrDefault();


            var pedido = new Pedidos
            {
                ClienteId = clientes.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutosId = produtos.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    },
                    new PedidoItem
                    {
                        ProdutosId = produtos.Id,
                        Desconto = 0,
                        Quantidade = 3,
                        Valor = 50,
                    }
                }
            };
            db.Pedidos.Add(pedido);
            db.SaveChanges();

        }// Este método atualiza apenas o campo referenciado EX=clienteNome
        static void AtualizarDados()
        {
            using var db = new Commerce.Data.ApplicationContext();
            var cliente = db.Clientes.Find(1);
            cliente.Nome = "Diana Tonzar";

            //Verifica e atualiza todos os campos
            //db.Clientes.Update(cliente);
            db.SaveChanges();

        }
        static void RemoverRegistros()
        {
            var db = new Commerce.Data.ApplicationContext();
            var cliente = db.Clientes.Find(2);
            db.Clientes.Remove(cliente);

            db.SaveChanges();
        }


        //app.MapGet("/", () => InserirDados());
        //app.MapGet("/", () => InserirDadosEmMassa());
        //app.MapGet("/", () => ConsultarDados());
        //app.MapGet("/", () => CadastrarPedido());
        //app.MapGet("/", () => AtualizarDados());
        app.MapGet("/", () => RemoverRegistros());

        app.Run();
    }
}


