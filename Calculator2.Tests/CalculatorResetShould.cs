namespace Calculator2.Tests
{
    public class CalculatorResetShould
    {
        [Fact]
        public void AssignDefaultValuesToProperties()
        {
            Calculator sut = new()
            {
                Op = "*",
                Num1 = 3.5,
                Num2 = 5,
                Numbers2 = new double[3] { 5, 6, 7.5 },
            };

            sut.Reset();

            Assert.Equal(sut.DefaultOp, sut.Op);
            Assert.Equal(sut.DefaultNum1, sut.Num1);
            Assert.Equal(sut.DefaultNum2, sut.Num2);
            Assert.Equal(sut.DefaultNumbers2, sut.Numbers2);
        }
    }
}
