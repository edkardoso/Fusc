using Fusc.Library.Core.Interfaces;
using Fusc.Library.Validation;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fusc.Library.Test
{

    public class ValidationModelCollectionTest
    {
        [Fact]
        public void Filter_ShouldReturnObjectNotNull_WhenCollectionEmpty()
        {

            /// arrange
            var collection = new ValidationModelCollection();

            /// action
            var result = collection.Filter((error) => error != null && error.Severity.HasFlag(Severity.Error));

            /// assert
            Assert.NotNull(result);

        }

        [Fact]
        public void Filter_ShouldOnlReturnOnlyErrorsValidations()
        {
            /// arrange
            ValidationModelCollection collection = CreateCollectionGeneral();

            /// action
            var result = collection.GetErrors();

            /// assert
            Assert.Equal(5, result.Count);

        }

       

        [Fact]
        public void Filter_ShouldReturnOnlyValidationsNotErros()
        {
            /// arrange
            var collection = CreateCollectionGeneral();
          
            /// action
            var result = collection.GetNotErrors();

            /// assert
            Assert.Equal(3, result.Count);

        }


        private static ValidationModelCollection CreateCollectionGeneral()
        {
            return new ValidationModelCollection
            {
                FResultMessageFactory.FatalError("Error: to connect"),
                FResultMessageFactory.Unauthorized("Error: to authenticate user"),
                FResultMessageFactory.InvalidData("Error: to validate data"),
                FResultMessageFactory.Warning("warning message"),
                FResultMessageFactory.BusinessError("Error: business erros"),
                FResultMessageFactory.Forbidden("Error: Forbidden"),
                FResultMessageFactory.Created("Success"),
                FResultMessageFactory.Success("Success"),

            };
        }


    }
}
