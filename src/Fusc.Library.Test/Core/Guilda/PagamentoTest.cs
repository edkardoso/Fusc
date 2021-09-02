using Moq;
using Xunit;

namespace Fusc.Library.Test.Core.Guilda
{
    public class PagamentoTest
    {
        [Fact]
        public void PagamentoUseCase_NaoDeveProcessarPagamento_QuandoProdutoNaoExistir()
        {

            // arrange
            var servicoPagamento = new Mock<IGatewayPagamento>();
            servicoPagamento.Setup(s => s.Aprovado()).Returns(true);

            var servicoEstoque = new Mock<IServicoDeEstoque>();
            servicoEstoque.Setup(s => s.ObterEstoque(It.IsAny<string>()));

            var cliente = new Cliente("Peter Parker", "spider@dc.com");
            var pedido = new Pedido("FJBL", 254, 1);

            var validador = new PagamentoValidator(pedido, servicoEstoque.Object);
            var pagamentoUseCase = new PagamentoUseCase(cliente, servicoPagamento.Object, validador);

            // action
            pagamentoUseCase.Execute();

            //assert
            Assert.False(pagamentoUseCase.PagamentoEfetuado);


        }

        [Fact]
        public void PagamentoUseCase_NaoDeveProcessarPagamento_QuandoNaoHouverAprovacaoDoGatewayDePagamento()
        {

            // arrange
            var servicoPagamento = new Mock<IGatewayPagamento>();
            servicoPagamento.Setup(s => s.Aprovado()).Returns(false);

            var servicoEstoque = new Mock<IServicoDeEstoque>();
            servicoEstoque.Setup(s => s.ObterEstoque(It.IsAny<string>())).Returns(new Produto("FJBL", "Fone JBL", 1));

            var cliente = new Cliente("Peter Parker", "spider@dc.com");
            var pedido = new Pedido("FJBL", 254, 1);

            var validador = new PagamentoValidator(pedido, servicoEstoque.Object);
            var pagamentoUseCase = new PagamentoUseCase(cliente, servicoPagamento.Object, validador);

            // action
            pagamentoUseCase.Execute();

            //assert
            Assert.False(pagamentoUseCase.PagamentoEfetuado);


        }


        [Fact]
        public void PagamentoUseCase_DeveProcessarPagamento_E_EnviarParaExpedicao_QuandoProdutoEstiverDisponivel()
        {

            // arrange
            var servicoPagamento = new Mock<IGatewayPagamento>();
            servicoPagamento.Setup(s => s.Aprovado()).Returns(true);


            var servicoEstoque = new Mock<IServicoDeEstoque>();
            servicoEstoque.Setup(s => s.ObterEstoque(It.IsAny<string>())).Returns(new Produto("FJBL", "Fone JBL", 2));

            var cliente = new Cliente("Peter Parker", "spider@dc.com");
            var pedido = new Pedido("FJBL", 254, 1);

            var validador = new PagamentoValidator(pedido, servicoEstoque.Object);
            var expedicaoUseCase = new ExpedicaoUseCase(servicoEstoque.Object, pedido);
            var pagamentoUseCase = new PagamentoUseCase(cliente, servicoPagamento.Object, validador);

            pagamentoUseCase.SetNext(expedicaoUseCase);


            // action
            pagamentoUseCase.Execute();

            //assert
            Assert.True(pagamentoUseCase.PagamentoEfetuado);
            Assert.NotNull(expedicaoUseCase.Result.Data);


        }


        [Fact]
        public void PagamentoUseCase_NaoDeveProcessarPagamento_E_DeveCadastrarNotificaoParaAvisarPosteriomenteAoCliente_QuandoProdutoEstiverIndisponivel()
        {

            // arrange
            var servicoPagamento = new Mock<IGatewayPagamento>();
            servicoPagamento.Setup(s => s.Aprovado()).Returns(true);
            var servicoEstoque = new Mock<IServicoDeEstoque>();
            servicoEstoque.Setup(s => s.ObterEstoque(It.IsAny<string>())).Returns(new Produto("FJBL", "Fone JBL", 1));

            var cliente = new Cliente("Peter Parker", "spider@dc.com");
            var pedido = new Pedido("FJBL", 254, 2);

            var validador = new PagamentoValidator(pedido, servicoEstoque.Object);
            var notificacaoUseCase = new CadastroDeNotificacoesUseCase(pedido, cliente);
            var pagamentoUseCase = new PagamentoUseCase(cliente, servicoPagamento.Object, validador, notificacaoUseCase);

            // action
            pagamentoUseCase.Execute();

            //assert
            Assert.False(pagamentoUseCase.PagamentoEfetuado);
            Assert.NotNull(notificacaoUseCase.Result.Data);


        }

    }
}
