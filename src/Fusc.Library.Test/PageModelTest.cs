using Fusc.Library.Helper;
using Fusc.Library.Models;
using Xunit;

namespace Fusc.Library.Test
{


    public class PageModelTest
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Page_ShouldReturnValueZero_WhenZeroOrLess(int value)
        {
            //arrange
            var page = new PageModel(value, 0, 0);

            //action && assert
            Assert.Equal(0, page.Page);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(100)]
        public void Page_ShouldReturnValueEntered_WhenItIsValid(int value)
        {
            //arrange
            var page = new PageModel(value, 0, 0);

            //action && assert
            Assert.Equal(value, page.Page);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Take_ShoulReturnDefaultValue_WhenValueInformedIsEqualZeroOrLess(int value)
        {
            //arrange
            var takeDefault = 10;
            var page = new PageModel(0, value, takeDefault);

            //action && assert
            Assert.Equal(takeDefault, page.Take);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(100)]
        public void Take_ShoulReturnDefaultValue_WhenValueEnteredIsGreater(int value)
        {
            //arrange
            var takeDefault = 10;
            var page = new PageModel(0, value, takeDefault);

            //action && assert
            Assert.Equal(takeDefault, page.Take);
        }

        [Fact]
        public void Take_ShoulddReturnValueEntered_WhenItIsValid()
        {
            //arrange
            var takeDefault = 10;
            var page = new PageModel(0, 9, takeDefault);

            //action && assert
            Assert.Equal(9, page.Take);
        }


        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        public void Skip_ShouldValueZero_WhenPageIsEqualZeroOrLess(int pageInit, int take)
        {
            //arrange
            var takeDefault = 10;
            var page = new PageModel(pageInit, take, takeDefault);

            //action && assert
            Assert.Equal(0, page.Skip);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 5)]
        [InlineData(1, 10)]
        [InlineData(1, 50)]
        [InlineData(2, 30)]
        public void Skip_ShouldReturnResultMultiplicationPageAndTake_WhenValuesItIsValid(int pageInit, int take)
        {
            //arrange
            var takeDefault = 10;
            var page = new PageModel(pageInit, take, takeDefault);

            //action && assert
            Assert.Equal(page.Page * page.Take, page.Skip);
        }

     


    }
}
