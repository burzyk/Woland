namespace Woland.Tests
{
    using Xunit;
    using Woland.Service;

    public class SimpleTest
    {
        [Fact]
        public void SomeSimpleTest()
        {
            var x = new SimpleService();
            Assert.Equal(3, x.Add(1, 2));
        }
    }
}
