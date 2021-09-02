using Fusc.Library.Models;
using System.Linq;
using Xunit;

namespace Fusc.Library.Test
{
    public class ResponseModelTest
    {
        [Fact]
        public void AddNavigationLink_Should()
        {
            //arrange
            var first = "first";
            var next = "next";
            var prev = "prev";
            var last = "last";
            var uri = "api/v1/namecontroller";
            var response = ResponseModelFactory.OK();

            //action
            response.AddNavigationLink(1, 5, 14, uri, first, next, prev, last);

            //assert
            var element0 = response.Links.ElementAt(0);
            Assert.Equal(4, response.Links.Count);
            Assert.Contains(uri, element0.Href);
            Assert.Equal("GET", element0.Action);
            Assert.Equal("P_FST", element0.Code);
            Assert.Equal(first, element0.Rel);

 

        }
    }
}
