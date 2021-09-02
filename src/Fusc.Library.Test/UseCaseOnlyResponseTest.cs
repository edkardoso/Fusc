using Fusc.Library.Test.Helper;
using System.Net;
using Xunit;

namespace Fusc.Library.Test
{

    public class UseCaseOnlyResponseTest
    {
        // <method>_Should<expected>_When<condition>. 

        [Fact]
        public void Execute_ShouldObjectResultExecution()
        {
            ///arrange
            var usecase = new MyUseCaseA();

            ///action
            usecase.Execute();

            ///assert
            Assert.NotNull(usecase.Result);

        }

        [Fact]
        public void Execute_ShouldObjectResultExecution_WhenException()
        {
            ///arrange
            var usecase = new MyUseCaseB();

            ///action
            usecase.Execute();

            ///assert
            Assert.NotNull(usecase.Result);
            Assert.Equal(1, usecase.Result.Errors.Count);
            Assert.True(usecase.Result.Failure);

        }

        [Fact]
        public void Execute_ShouldReturnResponse200_WhenActionEventOfUseCaseIsSuccessful()
        {
            ///arrange
            var usecase = new MyUseCaseA();

            ///action
            usecase.Execute();

            ///assert
            Assert.Equal(HttpStatusCode.OK, usecase.Response.StatusCode);

        }


        [Fact]
        public void Execute_ShouldInvokeOnAction_WhenDontHaveValidator()
        {
            ///arrange
            var usecase = new MyUseCaseC();

            ///action
            usecase.Execute();

            ///assert
            Assert.NotNull(usecase.Result);

        }


        [Fact]
        public void Execute_ShouldInvokeOnAction_WhenDontHaveValidatorAndPassValueArgument()
        {
            ///arrange
            var usecase = new MyUseCaseC();

            ///action
            usecase.Execute(100);

            ///assert
            Assert.Equal(100,usecase.Result.Data);

        }

        [Fact]
        public void Execute_ShouldGetResultValue200_WhenValueHigherZero()
        {
            ///arrange
            var usecase = new MyUseCaseD();

            ///action
            usecase.Execute(100);

            ///assert
            Assert.Equal(200, usecase.Result.Data);

        }

        [Fact]
        public void Execute_ShouldGetResultValue0_WhenValueLessThanOrEqualZero()
        {
            ///arrange
            var usecase = new MyUseCaseD();

            ///action
            usecase.Execute(0);

            ///assert
            Assert.Equal(0, usecase.Result.Data);

        }

    }
}
