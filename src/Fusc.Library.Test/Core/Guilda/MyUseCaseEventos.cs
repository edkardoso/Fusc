using Fusc.Library.Core;
using Fusc.Library.Core.Interfaces;
using Fusc.Library.Core.Result;
using Fusc.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusc.Library.Test.Core
{
    internal class MyUseCaseEventos : UseCase<int, ResponseModel>
    {
        public List<string> Log { get; }  = new();

        public override IBuilder<ResponseModel> Build(IBuilder<ResponseModel> builder)
        {
            return builder
                   .OnBefore((u, input) => Log.Add($"Executado Evento OnBefore;"))
                   .OnAction((u, input) => {
                       Log.Add($"Executado Evento OnAction;");
                       return FResultFactory.Evaluate((int)input, (i) => i % 2 == 0);
                       })
                   .OnSuccess(u =>
                   {
                       Log.Add($"Executado Evento OnSuccess => {u.Result.Data} é par.");
                       return FResultFactory.WithSuccess("Par");
                   })
                   .OnFail(u =>
                   {
                       Log.Add($"Executado Evento OnFail => {u.Result.Data} é impar.");
                       return FResultFactory.WithFail("Ímpar");
                   })
                   .OnCompleted(u => Log.Add($"Executado Evento OnComplete;"));

        }
    }

}
