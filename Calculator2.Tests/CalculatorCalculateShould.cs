namespace Calculator2.Tests
{
    public class CalculatorCalculateShould
    {
        [Fact]
        public void AddNumbers()
        {
            Calculator sut = new()
            {
                Op = "+",
                Num1 = 3.5,
                Num2 = 5,
            };

            double result = sut.Calculate();

            Assert.Equal(8.5, result);
        }


        [Fact]
        public void AddMultipleNumbers()
        {
            Calculator sut = new()
            {
                Op = "+",
                Num1 = 3.5,
                Numbers2 = new double[3] { 5, 6, 7.5 },
            };

            double result = sut.Calculate();

            Assert.Equal(22, result);
        }


        [Fact]
        public void SubtractNumbers()
        {
            Calculator sut = new()
            {
                Op = "-",
                Num1 = 3.5,
                Num2 = 5,
            };

            double result = sut.Calculate();

            Assert.Equal(-1.5, result);
        }


        [Fact]
        public void SubtractMultipleNumbers()
        {
            Calculator sut = new()
            {
                Op = "-",
                Num1 = 3.5,
                Numbers2 = new double[3] { 5, 6, 7.5 },
            };

            double result = sut.Calculate();

            Assert.Equal(-15, result);
        }


        [Fact]
        public void MultiplyNumbers()
        {
            Calculator sut = new()
            {
                Op = "*",
                Num1 = 3.5,
                Num2 = 5,
            };

            double result = sut.Calculate();

            Assert.Equal(17.5, result);
        }


        [Fact]
        public void DivideNumbers()
        {
            Calculator sut = new()
            {
                Op = "/",
                Num1 = 3.5,
                Num2 = 5,
            };

            double result = sut.Calculate();

            Assert.Equal(0.7, result);
        }


        [Fact]
        public void CalculateRemainder()
        {
            Calculator sut = new()
            {
                Op = "%",
                Num1 = 3.5,
                Num2 = 3,
            };

            double result = sut.Calculate();

            Assert.Equal(0.5, result);
        }


        [Fact]
        public void ThrowDivideByZeroException()
        {
            Calculator sut = new();

            sut.Op = "/";
            sut.Num1 = 3.5;
            sut.Num2 = 0;

            DivideByZeroException e = Assert.Throws<DivideByZeroException>(() => sut.Calculate());
            Assert.Equal("Division by 0!", e.Message);

            sut.Op = "%";

            e = Assert.Throws<DivideByZeroException>(() => sut.Calculate());
            Assert.Equal("Division by 0!", e.Message);
        }


        [Fact]
        public void ThrowArgumentException()
        {
            Calculator sut = new()
            {
                Op = "*",
                Num1 = 3.5,
                Numbers2 = new double[3] { 5, 6, 7.5 },
            };

            ArgumentException e = Assert.Throws<ArgumentException>(() => sut.Calculate());
            Assert.Equal($"Invalid combination of operator and operand!", e.Message);

            sut.Op = "/";

            e = Assert.Throws<ArgumentException>(() => sut.Calculate());
            Assert.Equal($"Invalid combination of operator and operand!", e.Message);

            sut.Op = "%";

            e = Assert.Throws<ArgumentException>(() => sut.Calculate());
            Assert.Equal($"Invalid combination of operator and operand!", e.Message);
        }
    }
}
