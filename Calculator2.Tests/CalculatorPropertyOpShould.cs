namespace Calculator2.Tests
{
    public class CalculatorPropertyOpShould
    {
        [Fact]
        public void ThrowArgumentException()
        {
            Calculator sut = new();
            string invalidOp = "?";

            ArgumentException e = Assert.Throws<ArgumentException>(() => sut.Op = invalidOp);
            Assert.Equal($"Invalid operator: '{invalidOp}'", e.Message);
        }
    }
}
