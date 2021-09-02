using Fusc.Library.Core.Result;
using Fusc.Library.Validation;
using System.Net;
using System.Text;
using Xunit;

namespace Fusc.Library.Test.Core
{


    public class UseCaseWithOutValidatorTest
    {

        [Fact]
        public void EventoOnAction_DeveRetornarMensagemContendoSeuNome_QuandoInvocado()
        {
            ///arrange
            var useCase = new MyUseCaseOnAction();

            ///act
            useCase.Execute(new StringBuilder());

            ///assert
            var result = (useCase.Result.Data as StringBuilder).ToString();
            Assert.Contains("MyUseCaseOnAction", result);
            Assert.Contains("Action", result);

        }

        [Fact]
        public void EventoOnAction_DeveRetornarUmFResultMesmo_QuandoForRetornadoNulo()
        {
            ///arrange
            var useCase = new MyUseCaseReturnNull();

            ///act
            useCase.Execute();

            ///assert
            Assert.NotNull(useCase.Result);
            Assert.IsType<FResultNull>(useCase.Result);

        }

        [Fact]
        public void ExecuteOnExecption_DeveCaputurarErro_QuandoOcorrerUmaException()
        {
            ///arrange
            var useCase = new MyUseCaseException();

            ///act
            useCase.Execute(10);

            ///assert
            Assert.NotNull(useCase.Result);
            Assert.Equal(HttpStatusCode.InternalServerError, useCase.Response.StatusCode);
            Assert.False(useCase.Response.Success);
            Assert.Collection(useCase.Response.Messages,
                m =>
                {
                    Assert.Equal(Severity.FatalError.ToString().ToUpper(), m.Severity.ToUpper());
                    Assert.Contains("MyUseCaseException", m.Message);
                    Assert.Contains("System.DivideByZeroException: Attempted to divide by zero.", m.Message);
                }
            );

        }

        [Theory]
        [InlineData(10,"Par")]
        [InlineData(9,"Ímpar")]
        [InlineData(1,"Ímpar")]
        public void Eventos_DeveExecutarTodosOsEventosConfigurados(int value, string resultado)
        {
            // arrange
            var useCase = new MyUseCaseEventos();

            // action
            useCase.Execute(value);


            //assert
            Assert.Equal(resultado, (string)useCase.Result.Data);
            Assert.NotNull(useCase.Log);

        }
    }
}
