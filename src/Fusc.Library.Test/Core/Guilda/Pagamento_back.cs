using Fusc.Library.Core;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Models;
using Fusc.Library.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fusc.Library.Test.Core.Guilda
{

    //public interface IGatewayPagamento
    //{
    //    bool Aprovado();
    //}

    //public interface IServicoDeEstoque
    //{
    //    Produto ObterEstoque(string codProduto);
    //}


    //public record Produto(string Codigo, string Descricao, int Quantidade);

    //public record Pedido(string CodigoProduto, decimal Preco, int Quantidade);

    //public record Cliente(string Nome, string Email);

    //public record NF(string Numero, List<Produto> Produtos, DateTime DataEntrega);

    //public record Notificacao(string CodigoProduto, string EmailCliente);





    //public class PagamentoValidator : UseCaseValidator<Pedido>
    //{
    //    private readonly IServicoDeEstoque _servicoDeEstoque;

    //    public PagamentoValidator(Pedido input, IServicoDeEstoque servicoDeEstoque) : base(input)
    //    {
    //        _servicoDeEstoque = servicoDeEstoque;
    //    }

    //    public override UseCaseValidationResult Validate()
    //    {
    //        var produto = _servicoDeEstoque.ObterEstoque(Input.CodigoProduto);

    //        if (produto == null)
    //        {
    //            Result.Add("Produto inexistente.", Severity.BusinessError, "ER999");
    //            return Result;
    //        }

    //        if (produto.Quantidade < Input.Quantidade)
    //        {
    //            Result.Add(FResultMessageFactory.BusinessError("Quantidade indisponível", "ER100"));
    //        }

    //        return Result;

    //    }
    //}

    //public class CadastroDeNotificacoesUseCase : UseCase<string, ResponseModel>
    //{
    //    private readonly Pedido _pedido;
    //    private readonly Cliente _cliente;


    //    public CadastroDeNotificacoesUseCase(Pedido pedido, Cliente cliente)
    //    {
    //        _pedido = pedido;
    //        _cliente = cliente;
    //    }

    //    public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
    //    {
    //        return builder.OnAction((u, input) =>
    //        {
    //            return FResultFactory.WithSuccess(new Notificacao(_pedido.CodigoProduto, _cliente.Email));
    //        });
    //    }
    //}


    //public class PagamentoUseCase : UseCase<Pedido, ResponseModel>
    //{
    //    private readonly Cliente _cliente;
    //    private readonly CadastroDeNotificacoesUseCase _useCaseNotificacoes;
    //    private readonly IGatewayPagamento _servicoPagamento;

    //    public bool PagamentoEfetuado { get; private set; }

    //    public PagamentoUseCase(Cliente cliente, IGatewayPagamento servicoPagamento, IUseCaseValidator<Pedido> validator, CadastroDeNotificacoesUseCase useCaseNotificacoes = null) : base(validator)
    //    {
    //        _cliente = cliente;
    //        _useCaseNotificacoes = useCaseNotificacoes;
    //        _servicoPagamento = servicoPagamento;
    //    }

    //    public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
    //    {
    //        return builder.CaseIsNotValid((pedido, resultValidation) =>
    //        {
    //            if (resultValidation.Messages.Any(m => m.ErrorCode == "ER100"))
    //            {
    //                SetNext(_useCaseNotificacoes);
    //            }

    //            return FResultFactory.WithFail(stop: true);

    //        }).OnAction((u, pedido) =>
    //        {
    //            return FResultFactory.IsTrue(_servicoPagamento.Aprovado(), pedido);
    //        })
    //        .OnSuccess(u =>
    //        {
    //            PagamentoEfetuado = true;
    //            return FResultFactory.WithSuccess(u.Result.Data);


    //        })
    //        .OnFail(u =>
    //        {
    //            PagamentoEfetuado = false;
    //            return FResultFactory.WithFail(stop: true);
    //        });


    //    }
    //}


    //public class ExpedicaoUseCase : UseCase<Pedido, ResponseModel>
    //{
    //    private readonly IServicoDeEstoque _servicoDeEstoque;
    //    private readonly Pedido _pedido;

    //    public ExpedicaoUseCase(IServicoDeEstoque servicoDeEstoque, Pedido pedido)
    //    {
    //        _servicoDeEstoque = servicoDeEstoque;
    //        _pedido = pedido;
    //    }


    //    public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
    //    {
    //        return builder.OnAction((_, __) =>
    //        {
    //            var produto = _servicoDeEstoque.ObterEstoque(_pedido.CodigoProduto);

    //            return FResultFactory.WithSuccess(
    //                      new NF("02833"
    //                     , new List<Produto>() { produto }
    //                     , DateTime.Now.AddDays(1))
    //                   );
    //        });
    //    }
    //}

}
